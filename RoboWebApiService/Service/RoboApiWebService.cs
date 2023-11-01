using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RoboWebApiService.Model;
using RoboWebApiService.Interface;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;

namespace RoboWebApiService.Service
{
    public class RoboApiWebService : IRoboWebApiService
    {
        private IConfiguration _configuration;

        public RoboApiWebService(IConfiguration configuration)
        {            
            _configuration = configuration;
        }

        public async ValueTask<IEnumerable<UdemyModel>> BuscarUdemyGoogle()
        {
            var udemy = new UdemyModel();
            var udemyBuscado = await ExecutaBusca();

            if (udemy == null)
            {
                return udemyBuscado;
            }

            return udemyBuscado;
        }

        private async ValueTask<IEnumerable<UdemyModel>> ExecutaBusca()
        {
            try
            {
                using (var _driver = new ChromeDriver())
                {
                    ReadOnlyCollection<IWebElement> lst1, lst2;
                    PreencherListasCursos(_driver, out lst1, out lst2);

                    return await MapearLista(lst1, lst2);
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        private void PreencherListasCursos(ChromeDriver _driver, out ReadOnlyCollection<IWebElement> lst1, out ReadOnlyCollection<IWebElement> lst2)
        {
            _driver.Navigate().GoToUrl(_configuration["UrlSiteUdemy:Url"]);

            _driver.FindElement(By.Name("query")).SendKeys("logica");

            _driver.FindElement(By.XPath("/html/body/main/section[1]/header/div/nav/div[2]/form/button")).Click();

            Thread.Sleep(2000);

            lst1 = _driver.FindElements(By.TagName("h4"));
            lst2 = _driver.FindElements(By.ClassName("busca-resultado-descricao"));
        }

        private async ValueTask<IEnumerable<UdemyModel>> MapearLista(ReadOnlyCollection<IWebElement> lst1, ReadOnlyCollection<IWebElement> lst2)
        {
            var result = new List<UdemyModel>();

            for (var i = 0; i < lst2.Count; i++)
            {
                var cursoDadosCompleto = new UdemyModel
                {
                    Id = i + 1,
                    nome = lst1[i].Text,
                    descricao = lst2[i].Text
                };

                result.Add(cursoDadosCompleto);
            }

            return result;
        }

        private UdemyModel PrechimentoObjeto()
        {
            var udemyModel = new UdemyModel();

            TesteObjetos(udemyModel);

            udemyModel.descricao = "Mudei de idéia aqui";

            return udemyModel;
        }


        private UdemyModel TesteObjetos(UdemyModel udemyModel)
        {
            udemyModel.Id = 1;
            udemyModel.nome = "Teste 1";
            udemyModel.descricao = "Descrição 1";

            // Disparar Rabbit com o udemyModel preenchido

            return udemyModel;
        }




    }
}
