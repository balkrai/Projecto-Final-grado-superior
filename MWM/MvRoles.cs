using di.proyecto.clase.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace di.proyecto.clase.MWM
{
   public class MvRoles:MVBase
    {
        private incidenciasEntities incEnt;
        private RolServicio rolserv;
        private PermisosServicio permserv;
        private ListCollectionView listaRoles;
         private List<permisos> listaPermisos;
        private roles rolNuevo = new roles();
        private bool esEdit;


        public MvRoles(incidenciasEntities ent)
        {
            incEnt = ent;
            rolserv = new RolServicio(incEnt);
            permserv = new PermisosServicio(incEnt);
            listaRoles = new ListCollectionView(rolserv.getAll().ToList());
            listaPermisos = new List<permisos>(permserv.getAll().ToList());




        }

        /*guarda una variable boolean para comprobar si se va a editar*/
        public bool editarEs
        {
            get { return esEdit; }
            set
            {
                esEdit = value;
                OnPropertyChanged("editarEs");
            }



        }

        /*Guarda el nuevo rol o el rol a modificar*/
        public roles nuevoRol
        {
            get { return rolNuevo; }
            set
            {
                rolNuevo = value;
                OnPropertyChanged("nuevoRol");
            }



        }

        /*Lista usada para comprobar el permiso seleccionado*/
        public List<permisos> listDePermisos { get { return listaPermisos; } }
        /*Lista de roles para el datagrid*/
        public ListCollectionView listDeRoles { get { return listaRoles; } }

        /*Metodo para guardar el rol */
        public bool guarda()
        {
            bool correcto = true;

            try
            {
                if (esEdit == false)
                {
                    int id = rolserv.getLastId() + 1;
                    short rolId = Convert.ToInt16(id);
                    int idtipo = rolserv.getLastId() + 1;

                    rolserv.add(rolNuevo);
                    rolNuevo.idRoles = rolId;
                    rolserv.save();
                }
                else
                {
                    rolserv.edit(rolNuevo);
                    rolserv.save();
                    correcto = true;
                }

            }catch (DbUpdateException dbex)
            {
                correcto = false;
                System.Console.WriteLine(dbex.StackTrace + "    " + dbex.InnerException);
            }

            return correcto;

        }


        }
}
