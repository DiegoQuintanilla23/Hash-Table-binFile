using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    /**
     * Clase registro
     * Manipula los atributos de los registros para su lectura
     * Son registros de alumnos
     */
    public class Registro
    {
        //ESTE ES EL REGISTRO

        ///Direccion de archivo
        public long Dir;
        ///Clave del alumno
        public long Clave;
        ///Nombre del alumno
        public string Nombre;
        ///Carrera del alumno
        public string Carrera;

        /**
         * Constructor de registro
         * Recibe un long, y dos cadenas
         * Asigna valores iniciales para el registro y asigna a los atributos las variables recibidas
         */
        public Registro(long c, string n, string s)
        {
            Dir = -1;
            Clave = c;
            Nombre = n;
            Carrera = s;
        }

        /**
         * Constructor de registro
         * Recibe variables para cada uno de los atributos
         * Asigna estas variables a cada uno de los atributos
         * Usado para la lectura de registros
         */
        public Registro(long d, long c, string n, string s)
        {
            Dir = d;
            Clave = c;
            Nombre = n;
            Carrera = s;
        }

        /**
         * Asigna una direccion de archivo al atributo Dir
         */
        public void setDir(long d)
        {
            Dir=d;
        }

        /**
         * Regresa el atributo Dir (Direccion de archivo)
         */
        public long getDir()
        {
            return Dir;
        }

        /**
         * Regresa el atributo Clave
         */
        public long getClave()
        {
            return Clave;
        }

        /**
         * Regresa el atributo Nombre
         */
        public string getNomb()
        {
            return Nombre;
        }

        /**
         * Regresa el atributo Carrera
         */
        public string getCarr()
        {
            return Carrera;
        }
    }
}
