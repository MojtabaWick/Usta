using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CommentAgg.Contracts;
using Usta.Domain.Core.CommentAgg.Dtos;
using Usta.Domain.Core.CommentAgg.Entities;

namespace Usta.Domain.Service.CommentAgg
{
    public class CommentService(ICommentRepository commentRepo) : ICommentService
    {
        public async Task<PagedResult<CommentDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await commentRepo.GetAllAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<bool> ApproveAsync(int id, CancellationToken cancellationToken)
        {
            return await commentRepo.ApproveAsync(id, cancellationToken);
        }

        public async Task<bool> RejectAsync(int id, CancellationToken cancellationToken)
        {
            return await commentRepo.RejectAsync(id, cancellationToken);
        }

        public async Task<bool> CreateComment(CommentInputDto input, int expertId, CancellationToken cancellationToken)
        {
            var newComment = new Comment()
            {
                OrderId = input.OrderId,
                ExpertId = expertId,
                Text = input.Text,
                Rating = input.Rating
            };
            return await commentRepo.AddComment(newComment, cancellationToken);
        }
    }
}