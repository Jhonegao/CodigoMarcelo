using Common;
using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //Mais validações serão feitas terça-feira!
            return base.Validate(item);
        }

    }
}
