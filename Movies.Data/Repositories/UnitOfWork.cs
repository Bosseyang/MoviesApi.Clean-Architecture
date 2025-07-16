using Movies.Core.DomainContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieContext _context;
    public IMovieRepository Movies { get; }
    public IReviewRepository Reviews { get; }
    public IActorRepository Actors { get; }

    public UnitOfWork(MovieContext context)
    {
        _context = context;
        Movies = new MovieRepository(_context);
        Reviews = new ReviewRepository(_context);
        Actors = new ActorRepository(_context);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}
