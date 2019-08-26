using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs e)
        {
            string cep = CEP.Text.Trim();
            if (isValidCep(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                        RESULTADO.Text = $"Endereço: {end.Localidade}, {end.UF} {end.Logradouro} - {end.Bairro}";
                    else
                        DisplayAlert("Erro", "CEP informado é inválido.", "OK");

                }
                catch (Exception ex)
                {
                    DisplayAlert("Network error", $"Ocorreu um erro de rede: {ex.Message}", "OK");
                }

            }
        }

        private bool isValidCep(string cep)
        {
            bool valid = true;

            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido, deve ter 8 caracteres.", "OK");
                valid = false;
            }

            int NovoCEP = 0;
            if (!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido, O cep deve ter apenas numeros.", "OK");
                valid = false;
            }
            return valid;
        }
    }
}
