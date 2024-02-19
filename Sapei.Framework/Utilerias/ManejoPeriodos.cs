using System;
using System.Collections.Generic;

namespace Sapei.Framework.Utilerias
{
	public static class ManejoPeriodos
	{
		/// <summary>
		/// Periodo Anterior
		/// </summary>
		/// <param name="psPeriodo"></param>
		/// <param name="psIncluyeVerano"></param>
		/// <returns></returns>
		public static string PeriodoAnterior(this String psPeriodo, bool psIncluyeVerano = false)
		{
			int liPeriodo = Convert.ToInt32(psPeriodo.Substring(4, 1));
			int liAño;
			switch (liPeriodo)
			{
				case 1:
					liAño = Convert.ToInt32(psPeriodo.Substring(0, 4)) - 1;
					return Convert.ToString(liAño) + "3";
				case 3:
					if (psIncluyeVerano)
						return psPeriodo.Substring(0, 4) + "2";
					return psPeriodo.Substring(0, 4) + "1";
			}
			return "";
		}
		/// <summary>
		/// Periodo Siguiente
		/// </summary>
		/// <param name="psPeriodo"></param>
		/// <param name="psIncluyeVerano"></param>
		/// <returns></returns>
		public static string PeriodoSiguiente(this String psPeriodo, bool psIncluyeVerano = false)
		{
			int liPeriodo = Convert.ToInt32(psPeriodo.Substring(4, 1));
			int liAño;
			switch (liPeriodo)
			{
				case 1:
					if (psIncluyeVerano)
						liPeriodo = liPeriodo + 1;
					else
						liPeriodo = liPeriodo + 2;
					return psPeriodo.Substring(0, 4) + Convert.ToString(liPeriodo);
				case 2:
					return psPeriodo.Substring(0, 4) + Convert.ToString(liPeriodo + 1);
				case 3:
					liAño = Convert.ToInt32(psPeriodo.Substring(0, 4)) + 1;
					return Convert.ToString(liAño) + "1";

			}
			return psPeriodo;
		}
		public static bool EsVerano(this String psPeriodo)
		{
			if (psPeriodo.Substring(4, 1) == "2")
				return true;
			return false;
		}

		public static string RegresaDescripcionPeriodo(this String psPeriodo, string psSeparador = " ", bool pbSinAño = false)
		{
			int liPeriodo = Convert.ToInt32(psPeriodo.Substring(4, 1));
			string lsAño = psPeriodo.Substring(0, 4);
			if (pbSinAño)
				lsAño = "";
			switch (liPeriodo)
			{
				case 1: return "Ene-Jun" + psSeparador + lsAño;
				case 2: return "Verano" + psSeparador + lsAño;
				case 3: return "Ago-Dic" + psSeparador + lsAño;
			}
			return "";
		}
		public static string RegresaDescripcionLargaPeriodo(this String psPeriodo, string psSeparador = " ")
		{
			int liPeriodo = Convert.ToInt32(psPeriodo.Substring(4, 1));
			string lsAño = psPeriodo.Substring(0, 4);
			switch (liPeriodo)
			{
				case 1: return "Enero-Junio" + psSeparador + lsAño;
				case 2: return "Verano" + psSeparador + lsAño;
				case 3: return "Agosto-Diciembre" + psSeparador + lsAño;
			}
			return "";
		}
		public static List<Tuple<string,string, bool>> RegresaComboPeriodos(this String psPeriodo, string psPeriodoSeleccionado ,int piTop, bool pbIncluyeVerano = false, bool pbIncluyePeriodoSiguiente = true)
		{
			List<Tuple<string, string, bool>> loLista = new List<Tuple<string, string, bool>>();
			int liTemp = piTop;
			string lsPeriodoTemp = "";
			bool lbSelecionado = false;
			if (psPeriodo == psPeriodoSeleccionado)
				lbSelecionado = true;
			loLista.Add(new Tuple<string, string, bool>(psPeriodo, RegresaDescripcionPeriodo(psPeriodo), lbSelecionado));
			lbSelecionado = false;
			if (pbIncluyePeriodoSiguiente)
			{
				lsPeriodoTemp = PeriodoSiguiente(psPeriodo, pbIncluyeVerano);
				if (lsPeriodoTemp == psPeriodoSeleccionado)
					lbSelecionado = true;
				loLista.Add(new Tuple<string,string, bool>(lsPeriodoTemp, RegresaDescripcionPeriodo(lsPeriodoTemp), lbSelecionado)); 
			}
			
			while (liTemp > 1)
			{
				lsPeriodoTemp = PeriodoAnterior(psPeriodo, pbIncluyeVerano);
				lbSelecionado = false;
				if (lsPeriodoTemp == psPeriodoSeleccionado)
					lbSelecionado = true;
				loLista.Add(new Tuple<string, string, bool>(lsPeriodoTemp, RegresaDescripcionPeriodo(lsPeriodoTemp), lbSelecionado));
				liTemp -= 1;
			}

			return loLista;
		}

	}
}
