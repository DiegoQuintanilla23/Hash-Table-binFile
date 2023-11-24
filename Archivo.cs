using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    /**
     * Clase Archivo
     * Se encarga de escribir y leer del archivo la tabla, cubetas y registros
     * Se utilizan 3 en el programa para cada una de estas entidades
     */
    public class Archivo
    {
        ///Filestream
        public Stream fs;
        ///Binary Writer para escribir en los archivos
        public BinaryWriter bw;
        ///Binary Reader para leer de los archivos
        public BinaryReader binReader;
        ///Nombre de los archivos
        public string nombrear;

        /**
         * Constructor de archivo Principal
         * Recibe una cadena para el nombre del archivo
         * Inicializa Filestream, binary Writer y Reader
         */
        public Archivo(string ss)
        {
            nombrear = ss + ".dat";
            fs = new FileStream("./" + nombrear, FileMode.Create, FileAccess.ReadWrite);
            bw = new BinaryWriter(fs);
            binReader = new BinaryReader(fs);
        }

        /**
         * Constructor usado para la apertura de archivos
         * Recibe una cadena y un entero
         * Inicializa Filestream, Binary Writer y Reader
         */
        public Archivo(string ss, int i)
        {
            fs = new FileStream(ss, FileMode.Open, FileAccess.ReadWrite);
            bw = new BinaryWriter(fs);
            binReader = new BinaryReader(fs);
        }

        /**
         * Escribe la tabla secuencialmente en el archivo
         * Recibe un objeto Tabla
         */
        public void EscribirTabla(Tabla tb)
        {
            fs.Seek(0, SeekOrigin.Begin);
            bw.Write(tb.getNoCaj());
            bw.Write(tb.getNoRegCub());
            bw.Write(tb.getAPCV());
            for(int i=0;i<tb.getNoCaj();i++)
            {
                bw.Write(tb.getCajon(i));
            }
        }

        /**
         * Lee la tabla secuencialmente de una archivo
         * Recibe el Numero de cajones y el numero de registros admitidos por las cubetas
         * Regresa un objeto Tabla
         */
        public Tabla leerTabla(int NC, int NRC)
        {
            Tabla tb = new Tabla(NC, NRC);
            long[] cjs = new long[NC];
            fs.Seek(0, SeekOrigin.Begin);
            tb.NoCajones = binReader.ReadInt32();
            tb.NoRegistrosCub = binReader.ReadInt32();
            tb.PtrCV = binReader.ReadInt64();
            for (int i = 0; i < tb.Cajones.Length; i++)
            {
                cjs[i] = binReader.ReadInt64();
            }
            tb.Cajones = cjs;

            return tb;
        }

        /**
         * Escribe la cubeta secuencialmente en un archivo
         * Recibe un objeto Cubeta
         * Lo escribe al final del archivo
         */
        public long EscribirCubeta(Cubeta cb)
        {
            long pos;
            fs.Seek(0, SeekOrigin.End);
            pos = fs.Position;
            cb.setDir(pos);

            bw.Write(cb.Dir);
            bw.Write(cb.dirSigCub);
            bw.Write(cb.NoRegistrosAct);
            for(int i = 0;i<cb.Registros.Length;i++)
            {
                bw.Write(cb.Registros[i]);
            }
            
            return pos;
        }

        /**
         * Reescribe secuencialmente la cubeta en el archivo
         * Recibe un objeto Cubeta y un long (direccion de archivo)
         * Lo reescribe en la direccion recibida
         */
        public void reescribeCub(Cubeta cb,long cc)
        {
            fs.Seek(cc, SeekOrigin.Begin);
            bw.Write(cb.Dir);
            bw.Write(cb.dirSigCub);
            bw.Write(cb.NoRegistrosAct);
            for (int i = 0; i < cb.Registros.Length; i++)
            {
                bw.Write(cb.Registros[i]);
            }
            
        }

        /**
         * Lee la cubeta secuencialmente en el archivo
         * Recibe un long (direccion de archivo) y un int (numero de registros en cubeta)
         * Regresa un objeto Cubeta
         */
        public Cubeta leeCub(long dir, int NR)
        {
            Cubeta Cub = new Cubeta(NR);
            long[] REGS = new long[NR];

            fs.Seek(dir, SeekOrigin.Begin);

            Cub.setDir(binReader.ReadInt64());
            Cub.setsigCub(binReader.ReadInt64());
            Cub.setNRA(binReader.ReadInt32());
            for (int i = 0; i < Cub.Registros.Length; i++)
            {
                REGS[i] = binReader.ReadInt64();
            }
            Cub.Registros = REGS;
            
            return Cub;
        }

        /**
         * Escribe un registro secuencialmente en el archivo
         * Recibe un objeto registro
         * Lo escribe al final del archivo
         */
        public long escribirReg(Registro reg)
        {
            long pos;
            fs.Seek(0, SeekOrigin.End);
            pos = fs.Position;
            reg.setDir(pos);
            bw.Write(reg.getDir());
            bw.Write(reg.getClave());
            bw.Write(reg.getNomb());
            bw.Write(reg.getCarr());
            
            return pos;
        }

        /**
         * Lee el registro secuencialmente del archivo
         * Recibe un long (direccion de archivo)
         * Regresa un objeto Registro
         */
        public Registro leeReg(long dr)
        {
            Registro reg;
            long d;
            long cl;
            string nmb;
            string crr;

            fs.Seek(dr, SeekOrigin.Begin);
            d=binReader.ReadInt64();
            cl=binReader.ReadInt64();
            nmb=binReader.ReadString();
            crr=binReader.ReadString();

            reg = new Registro(d,cl,nmb,crr);
            return reg;
        }
    }
}
