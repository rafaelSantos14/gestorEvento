using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestorEvento.Services;
using GestorEvento.Models;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class FormEditarProduto : Form
    {
        private ProdutoService _service;
        private int _produtoId;

        public FormEditarProduto(int produtoId)
        {
            InitializeComponent();
            _produtoId = produtoId;
            _service = new ProdutoService();

            // Aplicar estilos dos botões
            EstiloManager.AplicarEstiloSalvar(btnSalvar);
            EstiloManager.AplicarEstiloLimpar(btnCancelar);

            // Carregar dados do produto
            CarregarProduto();
        }

        private void CarregarProduto()
        {
            try
            {
                var produto = _service.GetProductById(_produtoId);

                if (produto == null)
                {
                    DialogoCustomizado erro = new DialogoCustomizado(
                        "Erro",
                        "Produto não encontrado.",
                        TipoDialogo.Erro,
                        TipoButton.Ok
                    );
                    erro.ShowDialog();
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
                
                txtNome.Text = produto.Nome;
                txtNome.Focus();
                txtNome.SelectAll();
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar produto: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validar campo
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Por favor, preencha o nome do produto",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                txtNome.Focus();
                return;
            }

            // Criar objeto Produto para atualização
            var produto = new Produto
            {
                Id = _produtoId,
                Nome = txtNome.Text.Trim()
            };

            // Tentar atualizar no banco
            if (_service.UpdateProduct(produto))
            {
                DialogoCustomizado sucesso = new DialogoCustomizado(
                    "Sucesso",
                    "Produto atualizado com sucesso!",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                sucesso.ShowDialog();

                // Fechar form com resultado OK (para recarregar grid)
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            // Se falhar, o Service já mostra o erro via DialogoCustomizado
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
