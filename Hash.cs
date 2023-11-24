using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hash
{
    /**
     * Forms que contiene la interfaz grafica
     * Clase principal del programa
     * Contiene la tabla y los archivos para la manipulacion de la tabla en los archivos
     * Contiene las funciones para insercion y eliminacion
     */
    public partial class Hash : Form
    {
        ///Archivo de la tabla
        public Archivo TT;
        ///Archivo de cubetas
        public Archivo CC;
        ///Archivo de registros
        public Archivo RR;
        ///Archivo de la tabla
        public Tabla tab;
        ///Estilo para las celdas del datagrid
        DataGridViewCellStyle style1 = new DataGridViewCellStyle();
        ///Estilo para las celdas del datagrid
        DataGridViewCellStyle style2 = new DataGridViewCellStyle();
        ///Estilo para las celdas del datagrid
        DataGridViewCellStyle style3 = new DataGridViewCellStyle();
        ///Estilo para las celdas del datagrid
        DataGridViewCellStyle style4 = new DataGridViewCellStyle();

        ///VARIABLES PARA LA TABLA
        ///SE USA LA FUNCION HASH AL CUADRADO
        ///POR LO QUE EL CALCULO DE CAJONES SOLO DARA RESULTADOS
        ///EN UN RAGO DE 00 - 99
        public int NumCaj = 100;
        public int NumRegCub = 3;

        /**
         * Constructor del Forms Hash
         * Solo inicializa el forms
         */
        public Hash()
        {
            InitializeComponent();
        }

        /**
         * Funcion para cuando se presiona el boton de insertar
         * Hace validaciones y  verifica que existan valores en los campos de datos
         * Inserta el registro e imprime la tabla en los datagrid
         */
        private void Insert_Click(object sender, EventArgs e)
        {
            if(NombTB.Text != "" && CarrCB.SelectedIndex != -1 && cuTB.Text != "" && cuTB.Text.All(char.IsDigit))
            {
                long claveunica;
                string nombr, carrer;
                Tabla.Rows.Clear();
                Cubetas.Rows.Clear();

                claveunica = long.Parse(cuTB.Text);
                nombr = NombTB.Text;
                carrer = CarrCB.SelectedItem.ToString();

                Registro al = new Registro(claveunica, nombr, carrer);
                RR.escribirReg(al);

                Inserta_Registro(al);
                print();
                Contrr();

                cuTB.Text = "";
                NombTB.Text = "";
                TBElim.Text = "";
                CarrCB.SelectedIndex = -1;
            }
            else
            {
                string mensaje = "Ingresa bien todos los datos";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(mensaje, "Mensaje", buttons);
            }
        }

        /**
         * Funcion Principal para la Insercion de un registro
         * Recibe un registro
         * Hace verificaciones, decide si se crea una cubeta o si se utiliza una de las
         * cubetas vacias si las hay
         * Tambien verifica si se quiere insertar a una cubeta llena, si es asi, se crea una nueva
         * que sera la siguiente a la cubeta llena
         */
        public void Inserta_Registro(Registro al)
        {
            int cajon = tab.Hash(al.getClave());
            long dirC = tab.getCajon(cajon);
            Cubeta C;
            int enc=0;

            if (dirC == -1)
            {
                dirC = tab.getAPCV();
                if(dirC == -1)
                {
                    C = new Cubeta(NumRegCub);
                    dirC=CC.EscribirCubeta(C);
                }
                else
                {
                    C = CC.leeCub(dirC, NumRegCub);
                    tab.setCV(C.getsigCub());
                    C.setsigCub(-1);
                }

                C.insertaReg(al.getDir());
                C.AumNR();
                tab.setCajon(cajon, dirC);

                CC.reescribeCub(C, dirC);
                TT.EscribirTabla(tab);
            }
            else
            {
                do
                {
                    C = CC.leeCub(dirC, NumRegCub);
                    if (C.getRegAct() == NumRegCub)
                    {
                        dirC = C.getsigCub();
                    }
                    else
                    {
                        enc = 1;
                    }
                } while (enc==0 && dirC != -1);

                if(enc==1)
                {
                    C.insertaReg(al.getDir());
                    C.AumNR();
                    CC.reescribeCub(C,dirC);
                }else
                {
                    dirC = tab.getAPCV();
                    if(dirC==-1)
                    {
                        C = new Cubeta(NumRegCub);
                        dirC = CC.EscribirCubeta(C);
                    }
                    else
                    {
                        C = CC.leeCub(dirC, NumRegCub);
                        tab.setCV(C.getsigCub());
                        C.setsigCub(-1);
                    }
                    C.insertaReg(al.getDir());
                    C.AumNR();
                    C.setsigCub(tab.getCajon(cajon));
                    tab.setCajon(cajon, dirC);

                    CC.reescribeCub(C, dirC);
                    TT.EscribirTabla(tab);
                }
            }
        }

        /**
         * Funcion que se ejecuta al inicial el forms
         * Hace visibles y no visibles los paneles contenidos en el forms
         */
        private void Hash_Load(object sender, EventArgs e)
        {
            P1.Visible = true;
            P2.Visible = false;
            P3.Visible = false;
        }

        /**
         * Funcion para cargar datos en varios componentes del forms
         * Añade elementos al combobox
         * Muestra datos de la tabla en un label
         * Muestra y oculta varios paneles
         * le da color a las celdas de los datagrid
         */
        private void loadd()
        {
            CarrCB.Items.Add("Computacion");
            CarrCB.Items.Add("Sistemas");
            CarrCB.Items.Add("Arquitectura");
            CarrCB.Items.Add("Ingenieria Civil");
            CarrCB.Items.Add("Quimica");
            CarrCB.Items.Add("Fisica");

            Contrr();

            P1.Visible = false;
            P2.Visible = true;
            P3.Visible = true;

            CreaGrid();

            style1.BackColor = Color.Red;
            style1.ForeColor = Color.White;
            style1.Font = new Font(Cubetas.Font, FontStyle.Bold);
            style2.BackColor = Color.Blue;
            style2.ForeColor = Color.White;
            style2.Font = new Font(Cubetas.Font, FontStyle.Bold);
            style3.BackColor = Color.Green;
            style3.ForeColor = Color.White;
            style3.Font = new Font(Cubetas.Font, FontStyle.Bold);
            style4.BackColor = Color.Yellow;
            style4.ForeColor = Color.Black;
            style4.Font = new Font(Cubetas.Font, FontStyle.Bold);
        }

        /**
         * Muestra datos de la tabla en un label dentro del forms
         */
        public void Contrr()
        {
            Control.Text = "No. de Cajones: \n" + NumCaj + "\n" + "No. de registros en Cubetas: \n" + NumRegCub + "\n" + "Apuntador a Cubetas Vacias: \n" + tab.PtrCV;
        }

        /**
         * Le da nombre a las columnas de los data grid
         * Basicamente los inicialia
         */
        public void CreaGrid()
        {
            Tabla.Rows.Clear();
            Tabla.Columns.Clear();
            Cubetas.Rows.Clear();
            Cubetas.Columns.Clear();

            Tabla.Columns.Add("No.", "No.");
            Tabla.Columns.Add("Cajon", "Cajon");

            Cubetas.Columns.Add("Direccion", "Direccion");
            Cubetas.Columns.Add("Registros Act.", "Registros Act.");
            for (int i = 1; i < NumRegCub+1; i++)
            {
                Cubetas.Columns.Add("RegDir "+i, "RegDir " + i);
                Cubetas.Columns.Add("Clave " + i, "Clave " + i);
                Cubetas.Columns.Add("Nombre " + i, "Nombre " + i);
                Cubetas.Columns.Add("Carrera " + i, "Carrera " + i);
            }
            Cubetas.Columns.Add("DirSigCub", "DirSigCub");

            foreach (DataGridViewColumn column in Tabla.Columns)
            {
                column.Width = 80;
            }

            foreach (DataGridViewColumn column in Cubetas.Columns)
            {
                column.Width = 100;
            }
        }

        /**
         * Funcion con objetivo de mostrar todo en los datagrid
         * Lee de los tres archivos y la tabla todos y cada uno de los datos guardados
         * Añade renglones conforme va leyendo cubetas y registros
         */
        public void print()
        {
            Cubeta Caux;
            Registro Raux;
            long cajon;
            long []regs = new long[NumRegCub];
            int cnt1 = 0, cnt2 = 2;
            for(int i = 0;i < NumCaj;i++)
            {

                //Tabla
                Tabla.Rows.Add();
                Tabla.Rows[i].Cells[0].Value = i;
                cajon = tab.getCajon(i);
                if(cajon == -1)
                {
                    Tabla.Rows[i].Cells[1].Value = "Vacio!";
                    Tabla.Rows[i].Cells[0].Style = style1;
                    Tabla.Rows[i].Cells[1].Style = style1;
                }
                else
                {
                    Tabla.Rows[i].Cells[1].Value = cajon;
                    Tabla.Rows[i].Cells[0].Style = style3;
                    Tabla.Rows[i].Cells[1].Style = style3;
                }

                //Cubetas
                if (cajon != -1)
                {
                    do
                    {
                        Cubetas.Rows.Add();
                        Caux = CC.leeCub(cajon, NumRegCub);
                        Cubetas.Rows[cnt1].Cells[0].Value = Caux.getdir();
                        Cubetas.Rows[cnt1].Cells[0].Style = style3;
                        Cubetas.Rows[cnt1].Cells[1].Value = Caux.getRegAct();
                        Cubetas.Rows[cnt1].Cells[1].Style = style3;
                        regs = Caux.getArr();

                        for (int j = 0; j < NumRegCub; j++)
                        {
                            if (regs[j] >= 0)
                            {
                                Raux = RR.leeReg(regs[j]);
                                Cubetas.Rows[cnt1].Cells[cnt2].Value = Raux.getDir();
                                Cubetas.Rows[cnt1].Cells[cnt2].Style = style2;
                                cnt2++;
                                Cubetas.Rows[cnt1].Cells[cnt2].Value = Raux.getClave();
                                Cubetas.Rows[cnt1].Cells[cnt2].Style = style4;
                                cnt2++;
                                Cubetas.Rows[cnt1].Cells[cnt2].Value = Raux.getNomb();
                                Cubetas.Rows[cnt1].Cells[cnt2].Style = style2;
                                cnt2++;
                                Cubetas.Rows[cnt1].Cells[cnt2].Value = Raux.getCarr();
                                Cubetas.Rows[cnt1].Cells[cnt2].Style = style2;
                                cnt2++;
                            }
                            else
                            {
                                Cubetas.Rows[cnt1].Cells[cnt2].Value = "Vacio";
                                Cubetas.Rows[cnt1].Cells[cnt2].Style = style1;
                                cnt2++;
                                Cubetas.Rows[cnt1].Cells[cnt2].Value = "Vacio";
                                Cubetas.Rows[cnt1].Cells[cnt2].Style = style1;
                                cnt2++;
                                Cubetas.Rows[cnt1].Cells[cnt2].Value = "Vacio";
                                Cubetas.Rows[cnt1].Cells[cnt2].Style = style1;
                                cnt2++;
                                Cubetas.Rows[cnt1].Cells[cnt2].Value = "Vacio";
                                Cubetas.Rows[cnt1].Cells[cnt2].Style = style1;
                                cnt2++;
                            }
                        }
                        Cubetas.Rows[cnt1].Cells[cnt2].Value = Caux.getsigCub();
                        if(Caux.getsigCub() != -1)
                        {
                            Cubetas.Rows[cnt1].Cells[cnt2].Style = style3;
                        }else
                        {
                            Cubetas.Rows[cnt1].Cells[cnt2].Style = style1;
                        }
                        cnt2 = 2;
                        cnt1++;
                        cajon=Caux.getsigCub();
                    } while (Caux.getsigCub() != -1);
                }
            }
        }

        /**
         * Funcion con objetivo de pedirle al usuario 3 archivos y leerlos
         *  Al presionar el boton "Abrir archivos" pide 3 archivos
         *  Hace validaciones y lee los archivos
         *  al finalizar y hacer leido los 3 archivos satisfactoriamente
         *  extrae los datos de la tabla y los asigna a la tabla del programa
         *  imprime la tabla leida
         */
        private void AbrArch_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Elija el Archivo de la tabla, luego el de cubetas y al final el de registros", "Archivos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = ".\\";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                TT = new Archivo(ofd.FileName, 1);
                OpenFileDialog ofdx = new OpenFileDialog();
                ofdx.InitialDirectory = ".\\";
                if (ofdx.ShowDialog() == DialogResult.OK)
                {
                    CC = new Archivo(ofdx.FileName, 1);
                    OpenFileDialog ofdy = new OpenFileDialog();
                    ofdy.InitialDirectory = ".\\";
                    if (ofdy.ShowDialog() == DialogResult.OK)
                    {
                        RR = new Archivo(ofdy.FileName, 1);
                        tab = TT.leerTabla(NumCaj, NumRegCub);
                        NumCaj = tab.NoCajones;
                        NumRegCub = tab.NoRegistrosCub;
                        P1.Visible = false;
                        P2.Visible = true;
                        P3.Visible = true;
                        loadd();
                        print();
                    }else
                    {
                        MessageBox.Show("No se selecciono Archivo de registros", "Sin seleccion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }else
                {
                    MessageBox.Show("No se selecciono Archivo de cubetas", "Sin seleccion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }else
            {
                MessageBox.Show("No se selecciono Archivo de la tabla", "Sin seleccion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /**
         * Funcion para cuando se presiona el boton de eliminar
         * Hace validaciones y  verifica que existan valores en los campos de datos
         * Verifica si se elimina el registro o si no se pudo eliminar
         * Imprime la tabla resultante
         */
        private void EliminBTN_Click(object sender, EventArgs e)
        {
            if(TBElim.Text != "" && TBElim.Text.All(char.IsDigit))
            {
                long clave;
                int val;
                Tabla.Rows.Clear();
                Cubetas.Rows.Clear();

                clave = long.Parse(TBElim.Text);
                val = EliminaClave(clave);
                if(val == 0)
                {
                    string mensaje = "No se encontró la clave";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(mensaje, "Mensaje", buttons);
                }
                print();
                Contrr();

                cuTB.Text = "";
                NombTB.Text = "";
                TBElim.Text = "";
                CarrCB.SelectedIndex = -1;
            }
            else
            {
                string mensaje = "Ingresa una clave unica valida";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(mensaje, "Mensaje", buttons);
            }
        }

        /**
         * Funcion con el algoritmo principal para la eliminacion de registros
         * Recibe una clave del registro que se desea eliminar
         * Calcula el cajon y va a buscar a ese cajon
         * Verifica si existe ese cajon o no
         * Busca en donde se localiza el registro (cubeta e indice) y lo elimina
         * Si no se encuentra la clave, se cancela la operacion
         * Si la cubeta queda vacia, se reescribe le apuntador a cubetas vacias en
         * la tabla del programa
         */
        public int EliminaClave(long cl)
        {
            int cajon = tab.Hash(cl);
            long dirC = tab.getCajon(cajon);

            if(dirC == -1)
            {
                string mensaje = "La clave no existe!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(mensaje, "Mensaje", buttons);
            }
            else
            {
                int pos=-1;
                Cubeta C, Cant=new Cubeta(NumRegCub);
                Registro Raux;
                do
                {

                    C = CC.leeCub(dirC, NumRegCub);
                    for(int i = 0; i < C.Registros.Length; i++)
                    {
                        if(C.Registros[i] == -1)
                        {
                            return 0;
                        }
                        Raux=RR.leeReg(C.Registros[i]);
                        if(Raux.getClave()==cl)
                        {
                            pos = i;
                            break;
                        }else
                        {
                            pos = -1;
                        }
                    }
                    if(pos==-1)
                    {
                        Cant = C;
                        dirC = C.getsigCub();
                        if(dirC == -1)
                        {
                            return 0;
                        }
                    }
                }while(pos==-1);

                C.eliminaRegistro(pos);

                if(C.cubetaVa())
                {
                    if(dirC == tab.getCajon(cajon))
                    {
                        tab.setCajon(cajon, C.getsigCub());
                    }else
                    {
                        Cant.setsigCub(C.getsigCub());
                        CC.reescribeCub(Cant, Cant.Dir);
                    }
                    C.setsigCub(tab.getAPCV());
                    tab.setCV(dirC);
                    TT.EscribirTabla(tab);
                }
                CC.reescribeCub(C, dirC);
            }
            return 1;
        }

        /**
         * Funcion con el objetivo de crear una nueva tabla
         * Se ejeecuta al presionar el boton "nueva tabla"
         * Crea un objeto tabla y lo asigna a la tabla del programa
         * Crea los 3 archivos para la tabla, cubeta y registros
         * Escribe la tabla en el archivo
         */
        private void Nuevo_Click(object sender, EventArgs e)
        {
            tab = new Tabla(NumCaj, NumRegCub);
            TT = new Archivo("Tabla");
            TT.EscribirTabla(tab);
            CC = new Archivo("Cubetas");
            RR = new Archivo("Registros");
            loadd();
        }
    }
}
