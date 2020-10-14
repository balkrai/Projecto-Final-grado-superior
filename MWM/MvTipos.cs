using di.proyecto.clase.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace di.proyecto.clase.MWM
{
    class MvTipos : MVBase
    {
        private incidenciasEntities incEnt;
        private TipoHwServicio hwserv;
        private TipoSwServicio swserv;
        private ListCollectionView listaHardware;
        private ListCollectionView listaSoftware;
        private tiposhw tipohw;
        private tipossw tiposw;
        private bool eleccion;




        public MvTipos(incidenciasEntities ent)
        {
            incEnt = ent;
            hwserv = new TipoHwServicio(incEnt);
            swserv = new TipoSwServicio(incEnt);
            listaHardware = new ListCollectionView(hwserv.getAll().ToList());
            listaSoftware = new ListCollectionView(swserv.getAll().ToList());

        }
        /*Lista que contiene todos los tipo hardware*/
        public ListCollectionView listHardware { get { return listaHardware; } }
        /*Lista que contiene todos los tipo Software*/
        public ListCollectionView listSoftware { get { return listaSoftware; } }

        /*Metodo que guarda la incidencia hw nueva para editarla en el dialogo*/
        public tiposhw IncidenciaHwNueva
        {
            get { return tipohw; }
            set
            {
                tipohw = value;
                OnPropertyChanged("IncidenciaHwNueva");
            }



        }

        /*metodo de variable boolean para controlar que se ha seleccionado*/
        public bool Seleccion
        {
            get { return eleccion; }
            set
            {
                eleccion = value;
                OnPropertyChanged("Seleccion");
            }

        }
        /*Metodo que guarda la incidencia sw nueva para editarla en el dialogo*/

        public tipossw IncidenciaSwNueva
        {
            get { return tiposw; }
            set
            {
                tiposw = value;
                OnPropertyChanged("IncidenciaSwNueva");
            }

        }

        /*Metodo que comprueba de que tipo de incidencia se trata y lo modifica*/
        public bool modificarTipo()
        {
            bool correcto = false;
            try
            {

              
                if (eleccion == true)
                {
                    hwserv.edit(tipohw);
                    correcto = true;
                    hwserv.save();
                }
                else
                {
                    swserv.edit(tiposw);
                    correcto = true;
                    swserv.save();
                }

            }
            catch (DbUpdateException dbex)
            {
                MessageBox.Show("Error al actualizar", "Modificar Tipo", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            return correcto;

            }
    }
}
