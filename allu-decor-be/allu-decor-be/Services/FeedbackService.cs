using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace allu_decor_be.Services
{
    public interface IFeedbackSevice
    {
        IEnumerable<Feedback> GetAll();
        Feedback GetById(string id);
        void CreateFeedback(Feedback feedback);
        void UpdateFeedback(Feedback feedback);
        void DeleteFeedback(string id);
    }

    public class FeedbackService : IFeedbackSevice
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public FeedbackService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public void CreateFeedback(Feedback feedback)
        {
            feedback.Id = Guid.NewGuid().ToString();
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public void DeleteFeedback(string id)
        {
            Feedback feedback = getFeedbackById(id);
            _context.Feedbacks.Remove(feedback);
            _context.SaveChanges();
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _context.Feedbacks;
        }

        public Feedback GetById(string id)
        {
            return getFeedbackById(id);
        }

        public void UpdateFeedback(Feedback feedback)
        {
            Feedback foundFeedback = getFeedbackById(feedback.Id);
            foundFeedback.Content = feedback.Content;
            _context.Feedbacks.Update(foundFeedback);
            _context.SaveChanges();
        }

        private Feedback getFeedbackById(string id)
        {
            Feedback feedback = _context.Feedbacks.Find(id);
            if (feedback == null)
            {
                throw new KeyNotFoundException("Cannot find feedback with id: " + id);
            }
            return feedback;
        }
    }
}
