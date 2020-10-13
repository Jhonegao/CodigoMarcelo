using Common;
using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicalLayer.Extensions;

namespace BusinessLogicalLayer
{
    public class ClienteBLL : BaseValidator<Cliente>
    {
        private ClienteDAO clienteDAO = new ClienteDAO();

        public Response Insert(Cliente item)
        {
            //Chama o método de validação do cliente
            Response response = Validate(item);
            //Se a validação passar:
            if (response.Success)
            {
                //Em caso de controle de log, poderiamos ter algo parecido com isso!
                //Response dbResponse = clienteDAO.Insert(item);
                //if (!dbResponse.Success)
                //{
                //    //LogError(dbResponse);
                //}
                //Chamar o método que insere o cliente no banco!
                return clienteDAO.Insert(item);
            }
            //Retornar o erro para o cliente
            return response;
        }

        public Response Update(Cliente item)
        {
            //Chama o método de validação do cliente
            Response response = Validate(item);
            //Se a validação passar:
            if (response.Success)
            {
                //Em caso de controle de log, poderiamos ter algo parecido com isso!
                //Response dbResponse = clienteDAO.Insert(item);
                //if (!dbResponse.Success)
                //{
                //    //LogError(dbResponse);
                //}
                //Chamar o método que insere o cliente no banco!
                return clienteDAO.Update(item);
            }
            //Retornar o erro para o cliente
            return response;
        }

        public Response Delete(Cliente item)
        {
            return clienteDAO.Delete(item);
        }

        public QueryResponse<Cliente> GetAll()
        {
            QueryResponse<Cliente> responseClientes = clienteDAO.GetAll();
            List<Cliente> temp = responseClientes.Data;
            foreach (Cliente item in temp)
            {
                item.CPF = item.CPF.Insert(3, ".").Insert(7, ".").Insert(12, "-");
            }
            return responseClientes;
        }

        public override Response Validate(Cliente item)
        {
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                AddError("O nome deve ser informado.");
            }
            else if (item.Nome.Length < 3 || item.Nome.Length > 70)
            {
                AddError("O nome deve conter entre 3 e 70 caracteres.");
            }

            AddError(item.CPF.IsValidCPF());

            if (!string.IsNullOrWhiteSpace(item.CPF))
            {
                //Garante ao menos que o CPF foi informado antes de remover a máscara
                item.CPF = item.CPF.Replace(".", "").Replace("-", "");
            }


            AddError(item.CNH.IsValidCNH());

            Response responseCPF = clienteDAO.IsCpfUnique(item.CPF);
            if (!responseCPF.Success)
            {
                AddError(responseCPF.Message);
            }

            return base.Validate(item);
        }

    }
}
