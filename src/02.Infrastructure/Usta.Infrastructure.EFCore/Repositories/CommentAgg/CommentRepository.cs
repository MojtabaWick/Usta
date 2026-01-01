using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CommentAgg.Contracts;
using Usta.Domain.Core.CommentAgg.Dtos;
using Usta.Infrastructure.EFCore.Persistence;

namespace Usta.Infrastructure.EFCore.Repositories.CommentAgg
{
    public class CommentRepository(AppDbContext dbContext) : ICommentRepository
    {
        public async Task<PagedResult<CommentDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = dbContext.Comments.AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new CommentDto
                {
                    Id = x.Id,
                    Text = x.Text,
                    Rating = x.Rating,
                    IsApproved = x.IsApproved
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<CommentDto>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<bool> ApproveAsync(int id, CancellationToken cancellationToken)
        {
            var affectedRow = await dbContext.Comments.Where(c => c.Id == id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(c => c.IsApproved, true), cancellationToken);

            return affectedRow > 0;
        }

        public async Task<bool> RejectAsync(int id, CancellationToken cancellationToken)
        {
            var affectedRow = await dbContext.Comments.Where(c => c.Id == id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(c => c.IsApproved, false), cancellationToken);

            return affectedRow > 0;
        }
    }
}