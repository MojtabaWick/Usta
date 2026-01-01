using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CommentAgg.Dtos;

namespace Usta.Domain.Core.CommentAgg.Contracts
{
    public interface ICommentService
    {
        Task<PagedResult<CommentDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<bool> ApproveAsync(int id, CancellationToken cancellationToken);

        Task<bool> RejectAsync(int id, CancellationToken cancellationToken);
    }
}