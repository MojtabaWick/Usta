using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CommentAgg.Contracts;
using Usta.Domain.Core.CommentAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Contracts;

namespace Usta.Domain.AppService.CommentAgg
{
    public class CommentAppService(ICommentService commentService, IOrderService orderService, ILogger<CommentAppService> _logger) : ICommentAppService
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

        public async Task<Result<bool>> CreateComment(CommentInputDto input, CancellationToken cancellationToken)
        {
            var hasComment = await orderService.OrderHasComment(input.OrderId, cancellationToken);
            if (hasComment)
                return Result<bool>.Failure("نمیتوان بیش از یک نظر برای هر سفارش ثبت کرد.");

            var orderIsCompleted = await orderService.OrderIsCompleted(input.OrderId, cancellationToken);
            if (!orderIsCompleted)
                return Result<bool>.Failure("خظای ثبت کامنت:سفارش باید پایان یافته باشد.");

            var expertId = await orderService.GetExpertIdByOrderId(input.OrderId, cancellationToken);

            var result = await commentService.CreateComment(input, expertId, cancellationToken);
            return result ? Result<bool>.Success("نظر شما با موفقیت ثبت شد. سپاس از شما!")
                : Result<bool>.Failure("ثبت نظر با خطا مواجه شده است.");
        }
    }
}