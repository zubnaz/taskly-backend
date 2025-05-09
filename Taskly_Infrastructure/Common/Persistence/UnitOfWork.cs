using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository; 
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Repositories;
using Taskly_Infrastructure.Services;

namespace Taskly_Infrastructure.Common.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private TasklyDbContext _context;
    private UserManager<UserEntity> _userManager;
    private readonly IRuleEvaluatorService _ruleEvaluatorService;

    public IAuthenticationRepository Authentication { get; private set; }
    public IBoardRepository Board { get; private set; }
    public IAvatarRepository Avatar { get; private set; }
    public ITableRepository Table { get; private set; }
    public ITableItemsRepository TableItems { get; private set; }
    public IBoardTemplateRepository BoardTemplates { get; private set; }
    public ICardRepository Cards { get; private set; }
    public IFeedbackRepository Feedbacks { get; private set; }
    public IAchievementRepository Achievements { get; private set; }
    public IChallengeRepository Challenges { get; private set; }
    public IInviteRepository Invites { get; private set; }
    public IBadgeRepository Badges { get; private set; }
    public IUserLevelRepository UserLevels { get; private set; }
    public IUserBadgeRepository UserBadges { get; private set; }

    public UnitOfWork(TasklyDbContext context,
        UserManager<UserEntity> userManager,
        ITableItemsRepository tableItemsRepository, 
        IFeedbackRepository feedbackRepository,
        IInviteRepository inviteRepository)    
    {
        _context = context;
        _userManager = userManager;
        _ruleEvaluatorService = new RuleEvaluatorService(tableItemsRepository, feedbackRepository, inviteRepository); 
        Authentication = new AuthenticationRepository(_userManager, _context);
        Board = new BoardRepository(_context);
        Avatar = new AvatarRepository(_context);
        Table = new TableRepository(_context);
        TableItems = new TableItemsRepository(_context);
        BoardTemplates = new BoardTemplateRepository(_context);
        Cards = new CardRepository(_userManager, _context);
        Feedbacks = new FeedbackRepository(_context);
        Achievements = new AchievementRepository(_context);
        Challenges = new ChallengeRepository(_context, _ruleEvaluatorService);
        Invites = new InviteRepository(_context);
        Badges = new BadgeRepository(_context);
        UserLevels = new UserLevelRepository(_context);
        UserBadges = new UserBadgeRepository(_context);
    }

    public async Task SaveChangesAsync(string errorMessage)
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException(errorMessage, ex);
        }
    }
}