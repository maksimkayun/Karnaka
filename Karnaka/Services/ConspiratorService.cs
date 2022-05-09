using AutoMapper;
using Karnaka.Data;
using Karnaka.Data.Models;
using Karnaka.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Karnaka.Services;

public class ConspiratorService : IConspiratorService
{
    private readonly KarnakaContext _context;
    private readonly IMapper _mapper;
    public ConspiratorService(KarnakaContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ConspiratorDto GetConspirator(int id)
    {
        return _mapper.Map<ConspiratorDto>(_context.Conspirators.Include(e=>e.Location)
            .Include(e=>e.PartPlan).SingleOrDefault(e => e.Id == id));
    }

    public ICollection<ConspiratorDto> GetAllConspirators()
    {
        return _mapper.Map<ICollection<ConspiratorDto>>(_context.Conspirators.Include(e=>e.Location)
            .Include(e=>e.PartPlan).Select(e => e));
    }

    public ConspiratorDto UpdateConspirator(ConspiratorDto conspirator, int id)
    {
        var upd = _context.Conspirators.SingleOrDefault(e => e.Id == id);
        upd = _mapper.Map<Conspirator>(conspirator);
        upd.Id = id;
        _context.Conspirators.Update(upd);
        _context.SaveChanges();
        return _mapper.Map<ConspiratorDto>(upd);
    }

    public ConspiratorDto AddConspirator(ConspiratorDto conspirator)
    {
        Conspirator cons = _mapper.Map<Conspirator>(conspirator);
        if (_context.Locations.SingleOrDefault(e=>
                e.Name.Equals(cons.Location.Name) &&
                e.Island.Equals(cons.Location.Island) &&
                e.City.Equals(cons.Location.City)) != default)
        {
            cons.Location = _context.Locations.SingleOrDefault(e=>
                e.Name.Equals(cons.Location.Name) &&
                e.Island.Equals(cons.Location.Island) &&
                e.City.Equals(cons.Location.City));
            
            cons = _context.Conspirators.Add(cons).Entity;
        }
        else
        {
           cons = _context.Conspirators.Add(_mapper.Map<Conspirator>(conspirator)).Entity;
        }
        
        _context.SaveChanges();
        return _mapper.Map<ConspiratorDto>(cons);
    }
    
    public ConspiratorDto DeleteConspirator(int id)
    {
        var cons = _context.Conspirators.SingleOrDefault(e => e.Id == id);
        _context.Conspirators.Remove(cons);
        _context.SaveChanges();
        return _mapper.Map<ConspiratorDto>(cons);
    }
}