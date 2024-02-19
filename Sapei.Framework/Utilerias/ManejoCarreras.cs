using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.Utilerias
{
    public static class ManejoCarreras
    {
        /// <summary>
        /// Periodo Anterior
        /// </summary>
        /// <param name="psPeriodo"></param>
        /// <param name="psIncluyeVerano"></param>
        /// <returns></returns>
        public static string RegresaNombreCompeto(this enmCarreras poCarrera)
        {
            switch (poCarrera)
            {
                case enmCarreras.ISI:
                    return "INGENIERÍA EN SISTEMAS COMPUTACIONALES";
                case enmCarreras.ARQ:
                    return "ARQUITECTURA";
                case enmCarreras.ISA:
                    return "INGENIERÍA EN SISTEMAS AUTOMOTRICES";
                case enmCarreras.IEL:
                    return "INGENIERÍA ELECTRÓNICA";
                case enmCarreras.IMC:
                    return "INGENIERÍA MECATRÓNICA";
                case enmCarreras.IFE:
                    return "INGENIERÍA FERROVIARIA";
                case enmCarreras.ADM:
                    return "LICENCIATURA EN ADMINISTRACIÓN ";
                case enmCarreras.COP:
                    return "CONTADOR PÚBLICO ";

            }
            return null;
        }

    }
}

