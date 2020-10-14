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
    public class MVVMIncidencias: MVBase
    {
        private incidenciasEntities incEnt;
        private string txtFiltro;
        private bool eleccion;
        private incidencias incidenciaNueva;
        private ProfesorServicio profserv;
        private tipossw SW;
        private IncidenciasServicio incserv;
        private EstadosServicio estServ;
        private tiposhw HW;
        private TipoHwServicio hwserv;
        private TipoSwServicio swserv;
        private String tipoAlternativo;
        private ListCollectionView listaIncidencias;
        public MVVMIncidencias(incidenciasEntities ent)
        {
            incEnt = ent;
            incserv = new IncidenciasServicio(incEnt);
            inicializa();
        }
        /*Metodo para inicializar todas las variables necesarias para controlar todo el tema de las
         incidencias*/
        public void inicializa()
        {
            incidenciaNueva = new incidencias();
            HW = new tiposhw();
            


             SW = new tipossw();
            estServ = new EstadosServicio(incEnt);
            hwserv = new TipoHwServicio(incEnt);
            swserv = new TipoSwServicio(incEnt);
            tipoAlternativo = "";
            listaIncidencias = new ListCollectionView(incserv.getAll().ToList());
            profserv = new ProfesorServicio(incEnt);
        }

        /*Lista de incidencias para el datagrid*/
        public ListCollectionView listaincidencias { get { return listaIncidencias; } }

        /*Lista de estados para el combobox*/
        public List<String> listaEstados
        {
            get { return new List<string> { "Enviada", "En proceso", "Acabada" }; }

        }


        /*Lista de tipos para el combobox*/

        public List<String> listaTipos
        {
            get { return new List<string> { "HW", "SW" }; }

        }
        public List<estados> listaestados { get { return estServ.getAll().ToList(); } }

        /*Lista de profesores para el arbol*/
        public List<profesores> listaprofesores { get { return profserv.getAll().ToList(); } }
        /*metodo para controlar si se modifica*/
        public Boolean modificar { get; set; }

        /*Lista de tipo hw a mostrar en el combobox*/
        public List<String> listaTiposHW
        {
            get { return new List<string> { "Servidor", "Ordenador","Monitor"
                ,"Impresora","Router","Switch","Proyector","Otro" }; }

        }

        /*metodo string que guarda el tipo alternativo*/
        public String tipoAlt
        {
            get { return tipoAlternativo; }
            set
            {
                tipoAlternativo = value;
                OnPropertyChanged("tipoAlt");
            }

        }


        /*Metodo que guarda la seleccion para controlar del tipo de incidencia de la que se trata*/
        public bool Seleccion
        {
            get { return eleccion; }
            set
            {
                eleccion = value;
                OnPropertyChanged("Seleccion");
            }

        }

        /*Incidencia a insertar*/
        public incidencias IncidenciaNueva
        {
            get { return incidenciaNueva; }
            set
            {
                incidenciaNueva = value;
                OnPropertyChanged("IncidenciaNueva");
            }

        }
        /*tipo hardware a insertar*/

        public tiposhw tipoHW
        {
            get { return HW; }
            set
            {
                HW = value;
                OnPropertyChanged("tipoHW");
            }

        }
        /*tipo software a insertar*/

        public tipossw tipoSW
        {
            get { return SW; }
            set
            {
                SW = value;
                OnPropertyChanged("tipoSW");
            }

        }

        /*Metodo para modificar incidencia */
        public bool ModificarIncidencia()
        {
            bool correcto = true;
            try
            {

                if (modificar == true)
            {
                incserv.edit(incidenciaNueva);
                correcto = true;
                }
            }

            catch (DbUpdateException dbex)
            {
                correcto = false;
                MessageBox.Show("Error al actualizar", "Insertar incidencia", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            incserv.save();
            return correcto;
        }

        /*Metodo para guardar la incidencia, depentiendo del tipo seleccionado se crea un registro en
         una tabla u en otra*/
        public bool guarda()
        {
            bool correcto = true;
            incserv.add(incidenciaNueva);
            int id = incserv.getLastId() + 1;
            short incId = Convert.ToInt16(id);
            int idtipo;

            IncidenciaNueva.idincidencias = incId;
            try
            {            
                 incserv.save();

                if (eleccion)
                {
                    hwserv.add(HW);

                    if (!tipoAlternativo.Equals(""))
                    {
                        HW.Tipo = tipoAlternativo;

                    }
                    
                    HW.idIncidencia = incId;

                    idtipo = hwserv.getLastId() + 1;
                   
                    HW.idTipos = Convert.ToInt16(idtipo); 
                    hwserv.save();
                }
                else
                {
                    swserv.add(SW);
                    SW.incidencias_idincidencias = incId;

                    idtipo = swserv.getLastId() + 1;

                    SW.idTipos = Convert.ToInt16(idtipo);

                    swserv.save();
                }

              

            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                System.Console.WriteLine(dbex.StackTrace + "    " + dbex.InnerException);
            }
            return correcto;
        }

        public ListCollectionView listaIncidenciasTabla { get { return listaIncidencias; } }

        /*Metodo que guarda el texto para el filtro de la descripcion*/
        public string textoFiltroDescripcion
        {
            get { return txtFiltro; }
            set
            {
                txtFiltro = value;
                OnPropertyChanged("textoFiltroDescripcion");
            }
        }

    }
}
