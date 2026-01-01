using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CommentAgg.Contracts;
using Usta.Domain.Core.CommentAgg.Dtos;

namespace Usta.Domain.AppService.CommentAgg
{
    public class CommentAppService(ICommentService commentService, ILogger<CommentAppService> _logger) : ICommentAppService
    {
        public async Task<PagedResult<CommentDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            _logger.LogInformation("getting all comments.");
            return await commentService.GetAllAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<bool> ApproveAsync(int id, CancellationToken cancellationToken)
        {
            var result = await commentService.ApproveAsync(id, cancellationToken);
            if (result)
            {
                _logger.LogInformation($"comment with id:{id} set approved.");
            }

            return result;
        }

        public async Task<bool> RejectAsync(int id, CancellationToken cancellationToken)
        {
            var result = await commentService.RejectAsync(id, cancellationToken);
            if (result)
            {
                _logger.LogInformation($"comment with id:{id} set approved.");
            }

            return result;
        }
    }
}