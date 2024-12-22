using AutoMapper;
using Entities;
using Infrastructures.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using UseCase.Repositories;
using UseCases.Repositories;

namespace Infrastructures.SQLServer.Repositories;

public class UserRepository : GenericRepository<User, UserEntity>, IUserRepository
{
    private readonly SchoolContext _context;
    private readonly IMapper _mapper;
    public UserRepository(SchoolContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User> FindUserByUsernameAndPassword(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        return _mapper.Map<User>(user);
    }
}
