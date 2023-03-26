using MediatR;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System.Threading.Tasks;
using System.Threading;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Linq;
using System;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoRepository;
        private readonly IProdutoRepository _produtoRepository;
        public RegistrarCompraCommandHandler(IProdutoRepository produtoRepository, ISolicitacaoCompraRepository solicitacaoRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            this._solicitacaoRepository = solicitacaoRepository;
            this._produtoRepository = produtoRepository;
        }
        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacao = new SolicitacaoAgg.SolicitacaoCompra(request.usuarioSolicitante, request.nomeFornecedor);
            var itens = request.itens.Select(item => new Item(_produtoRepository.Obter(new Guid(item.ProdutoId)), item.Qtde));

            solicitacao.RegistrarCompra(itens);
            _solicitacaoRepository.RegistrarCompra(solicitacao);

            Commit();
            PublishEvents(solicitacao.Events);

            return Task.FromResult(true);
        }
    }
}
