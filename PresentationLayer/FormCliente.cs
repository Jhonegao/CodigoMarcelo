using BusinessLogicalLayer;
using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class FormCliente : Form
    {
        public FormCliente()
        {
            InitializeComponent();
        }

        ClienteBLL clienteBLL = new ClienteBLL();

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.Nome = txtNome.Text;
            cliente.CPF = txtCPF.Text;
            cliente.CNH = txtCNH.Text;

            Response response = clienteBLL.Insert(cliente);
            MessageBox.Show(response.Message);

            if (response.Success)
            {
                //Só atualiza a grid se o insert funcionou.
                UpdateGridView();
            }
        }

        private void UpdateGridView()
        {
            QueryResponse<Cliente> response = clienteBLL.GetAll();
            if (response.Success)
            {
                dgvClientes.DataSource = response.Data;
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            //Ao inicializar o form, preencher a grid com os clientes do banco!
            UpdateGridView();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            FormCleaner.Clear(this);
        }
    }
}
