namespace Solution.Core.Interfaces;

public interface IMemberService
{
    Task<ErrorOr<MemberModel>> CreateAsync(MemberModel model);

    Task<ErrorOr<Success>> UpdateAsync(MemberModel model);

    Task<ErrorOr<Success>> DeleteAsync(string memberId);

    Task<ErrorOr<MemberModel>> GetByIdAsync(string memberId);

    Task<ErrorOr<List<MemberModel>>> GetAllAsync();

    Task<ErrorOr<PaginationModel<MemberModel>>> GetPagedAsync(int page = 0);
}
