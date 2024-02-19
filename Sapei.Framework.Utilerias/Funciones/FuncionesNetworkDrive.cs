using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Net;
using Sapei.Framework.Configuracion;

namespace Sapei.Framework.Utilerias.Funciones
{
	public class ConnectToSharedFolder : IDisposable
	{
		#region Consts
		const int RESOURCETYPE_DISK = 0x00000001;
		const int CONNECT_UPDATE_PROFILE = 0x00000001;
		#endregion

		readonly string _networkName;

		public ConnectToSharedFolder(string networkName, NetworkCredential credentials, enmSistema enmTipoSistema = enmSistema.PROCESO)
		{

			if (enmTipoSistema == enmSistema.PROCESO)
			{
				_networkName = networkName;

				var netResource = new NetResource
				{
					Scope = ResourceScope.GlobalNetwork,
					ResourceType = ResourceType.Disk,
					DisplayType = ResourceDisplaytype.Share,
					RemoteName = networkName
				};

				var userName = string.IsNullOrEmpty(credentials.Domain)
					? credentials.UserName
					: string.Format(@"{0}\{1}", credentials.Domain, credentials.UserName);

				var result = WNetAddConnection2(
				   netResource,
				   credentials.Password,
				   userName,
				   0);
				if (result != 0)
				{
					throw new Win32Exception(result, "Error connecting to remote share");
				}
				return;
			}

			//Create netresource and point it at the share
			NETRESOURCE nr = new NETRESOURCE();
			nr.dwType = RESOURCETYPE_DISK;
			nr.lpRemoteName = networkName;

			int ret = WNetUseConnection(IntPtr.Zero, nr, credentials.Password, credentials.UserName, 0, null, null, null);

			//Check for errors
			if (ret == NO_ERROR)
				return ;
			else
				throw new Exception(GetError(ret)); 

		}

		~ConnectToSharedFolder()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			WNetCancelConnection2(_networkName, 0, true);
		}

		[DllImport("mpr.dll")]
		private static extern int WNetAddConnection2(NetResource netResource,
			string password, string username, int flags);

		[DllImport("mpr.dll")]
		private static extern int WNetCancelConnection2(string name, int flags,
			bool force);

		[DllImport("Mpr.dll")]
		private static extern int WNetUseConnection(
			 IntPtr hwndOwner,
			 NETRESOURCE lpNetResource,
			 string lpPassword,
			 string lpUserID,
			 int dwFlags,
			 string lpAccessName,
			 string lpBufferSize,
			 string lpResult
			 );

		[DllImport("Mpr.dll")]
		private static extern int WNetCancelConnection(
			string lpName,
			bool fForce
			);

		[StructLayout(LayoutKind.Sequential)]
		public class NetResource
		{
			public ResourceScope Scope;
			public ResourceType ResourceType;
			public ResourceDisplaytype DisplayType;
			public int Usage;
			public string LocalName;
			public string RemoteName;
			public string Comment;
			public string Provider;
		}

		public enum ResourceScope : int
		{
			Connected = 1,
			GlobalNetwork,
			Remembered,
			Recent,
			Context
		};

		public enum ResourceType : int
		{
			Any = 0,
			Disk = 1,
			Print = 2,
			Reserved = 8,
		}

		public enum ResourceDisplaytype : int
		{
			Generic = 0x0,
			Domain = 0x01,
			Server = 0x02,
			Share = 0x03,
			File = 0x04,
			Group = 0x05,
			Network = 0x06,
			Root = 0x07,
			Shareadmin = 0x08,
			Directory = 0x09,
			Tree = 0x0a,
			Ndscontainer = 0x0b
		}

		[StructLayout(LayoutKind.Sequential)]
		private class NETRESOURCE
		{
			public int dwScope = 0;
			public int dwType = 0;
			public int dwDisplayType = 0;
			public int dwUsage = 0;
			public string lpLocalName = "";
			public string lpRemoteName = "";
			public string lpComment = "";
			public string lpProvider = "";
		}
		#region Errors
		const int NO_ERROR = 0;
		const int ERROR_ACCESS_DENIED = 5;
		const int ERROR_ALREADY_ASSIGNED = 85;
		const int ERROR_BAD_DEVICE = 1200;
		const int ERROR_BAD_NET_NAME = 67;
		const int ERROR_BAD_PROVIDER = 1204;
		const int ERROR_CANCELLED = 1223;
		const int ERROR_EXTENDED_ERROR = 1208;
		const int ERROR_INVALID_ADDRESS = 487;
		const int ERROR_INVALID_PARAMETER = 87;
		const int ERROR_INVALID_PASSWORD = 1216;
		const int ERROR_MORE_DATA = 234;
		const int ERROR_NO_MORE_ITEMS = 259;
		const int ERROR_NO_NET_OR_BAD_PATH = 1203;
		const int ERROR_NO_NETWORK = 1222;
		const int ERROR_SESSION_CREDENTIAL_CONFLICT = 1219;

		const int ERROR_BAD_PROFILE = 1206;
		const int ERROR_CANNOT_OPEN_PROFILE = 1205;
		const int ERROR_DEVICE_IN_USE = 2404;
		const int ERROR_NOT_CONNECTED = 2250;
		const int ERROR_OPEN_FILES = 2401;

		private struct ErrorClass
		{
			public int num;
			public string message;
			public ErrorClass(int num, string message)
			{
				this.num = num;
				this.message = message;
			}
		}

		private static ErrorClass[] ERROR_LIST = new ErrorClass[] {
	new ErrorClass(ERROR_ACCESS_DENIED, "Error: Access Denied"),
	new ErrorClass(ERROR_ALREADY_ASSIGNED, "Error: Already Assigned"),
	new ErrorClass(ERROR_BAD_DEVICE, "Error: Bad Device"),
	new ErrorClass(ERROR_BAD_NET_NAME, "Error: Bad Net Name"),
	new ErrorClass(ERROR_BAD_PROVIDER, "Error: Bad Provider"),
	new ErrorClass(ERROR_CANCELLED, "Error: Cancelled"),
	new ErrorClass(ERROR_EXTENDED_ERROR, "Error: Extended Error"),
	new ErrorClass(ERROR_INVALID_ADDRESS, "Error: Invalid Address"),
	new ErrorClass(ERROR_INVALID_PARAMETER, "Error: Invalid Parameter"),
	new ErrorClass(ERROR_INVALID_PASSWORD, "Error: Invalid Password"),
	new ErrorClass(ERROR_MORE_DATA, "Error: More Data"),
	new ErrorClass(ERROR_NO_MORE_ITEMS, "Error: No More Items"),
	new ErrorClass(ERROR_NO_NET_OR_BAD_PATH, "Error: No Net Or Bad Path"),
	new ErrorClass(ERROR_NO_NETWORK, "Error: No Network"),
	new ErrorClass(ERROR_BAD_PROFILE, "Error: Bad Profile"),
	new ErrorClass(ERROR_CANNOT_OPEN_PROFILE, "Error: Cannot Open Profile"),
	new ErrorClass(ERROR_DEVICE_IN_USE, "Error: Device In Use"),
	new ErrorClass(ERROR_EXTENDED_ERROR, "Error: Extended Error"),
	new ErrorClass(ERROR_NOT_CONNECTED, "Error: Not Connected"),
	new ErrorClass(ERROR_OPEN_FILES, "Error: Open Files"),
	new ErrorClass(ERROR_SESSION_CREDENTIAL_CONFLICT, "Error: Credential Conflict"),
};
		private static string GetError(int errNum)
		{
			foreach (ErrorClass er in ERROR_LIST)
			{
				if (er.num == errNum)
				{
					return er.message;
				}
			}
			return "Error: Unknown, " + errNum;
		}
		#endregion
	}
}
