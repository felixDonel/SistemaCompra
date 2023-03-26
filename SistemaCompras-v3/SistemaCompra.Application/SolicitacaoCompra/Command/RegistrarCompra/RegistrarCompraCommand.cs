using MediatR;
using SistemaCompra.Application.SolicitacaoCompra.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public string usuarioSolicitante { get; set; }
        public string nomeFornecedor { get; set; }
        public List<ItensDTO> itens { get; set; }
    }
}
