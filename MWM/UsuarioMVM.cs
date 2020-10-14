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
    class UsuarioMVM: MVBase
    {
        private incidenciasEntities incEnt;
        private ProfesorServicio profserv;
        private RolServicio rolserv;
        private DepartamentoServicio deptserv;
        private profesores profesorNuevo;
        private ListCollectionView listaprofesores;
        private bool esmodificar;
        private string txtFiltroNombre;



        public UsuarioMVM (incidenciasEntities Ent)
        {
            incEnt = Ent;
            profserv = new ProfesorServicio(incEnt);
            inicializa();
        }

        public void inicializa()
        {
            deptserv = new DepartamentoServicio(incEnt);
            rolserv = new RolServicio(incEnt);
            profesorNuevo = new profesores();
            listaprofesores = new ListCollectionView(profserv.getAll().ToList());
        }

        public List<departamento> listaDepartamento { get { return deptserv.getAll().ToList(); } }
        public List<roles> listaRoles { get { return rolserv.getAll().ToList(); } }

        /*Guarda el Nombre del profesor a filtrar*/
        public string textoFiltroNombre
        {
            get { return txtFiltroNombre; }
            set
            {
                txtFiltroNombre = value;
                OnPropertyChanged("textoFiltroNombre");
            }
        }

        /*Guarda el Usuario a insertar*/
        public profesores UsuarioNuevo
        {
            get { return profesorNuevo; }
            set
            {
                profesorNuevo = value;
                OnPropertyChanged("UsuarioNuevo");
            }

        }

        /*Permite saber si se va a modificar o no el usuario*/
        public bool esmodificarusu
        {
            get { return esmodificar; }
            set
            {
                esmodificar = value;
                OnPropertyChanged("esmodificarusu");
            }

        }

        /*Lista de estados de la cuenta*/
        public List<String> listaEstados
        {
            get { return new List<string> { "Activa", "No activa"}; }

        }
        /*Comprueba si el usuario es unico*/
        public bool usuarioUnico
        {
            get { return profserv.usuarioUnico(profesorNuevo.username); }
        }

        /*Permite guardar el usuario*/
        public bool guarda()
        {
            bool correcto = true;

           

                profserv.add(profesorNuevo);

                short profId = Convert.ToInt16(profserv.getLastId() + 1);
                profesorNuevo.idProfesores = profId;

                try
                {
                    profserv.save();
                }
                catch (DbUpdateException dbex)
                {
                    correcto = false;
                    System.Console.WriteLine(dbex.StackTrace + "    " + dbex.InnerException);
                }
            

            return correcto;
            }

        /*Permite editar el usuario*/
        public bool editar()
        {
            bool correcto = true;
            profserv.edit(profesorNuevo);

            try
            {
                profserv.save();
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                System.Console.WriteLine(dbex.StackTrace + "    " + dbex.InnerException);
            }
            return correcto;
        }

        public ListCollectionView listaUsuarios
        {
            get { return listaprofesores; }
        }

    }
}
