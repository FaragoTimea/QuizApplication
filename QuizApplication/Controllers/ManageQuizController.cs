using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Data;
using QuizApplication.DTO;

namespace QuizApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageQuizController : ControllerBase
    {
        private QuizDbContext _context;
        private IMapper _mapper;

        public ManageQuizController(
            QuizDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET all quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetAllAsync()
        {
            var quizes = await _context.Quizes.ToListAsync();

            return _mapper.Map<List<QuizDTO>>(quizes);
        }

        //GET quiz without questions
        [HttpGet("{quizId}")]
        public async Task<ActionResult<QuizDTO>> GetPreviewAsync(int quizId)
        {
            var quiz = await _context.Quizes
                .FindAsync(quizId);
            if (quiz is null) return BadRequest("Quiz doesnt exist");

            return _mapper.Map<QuizDTO>(quiz);
        }

        // GET byId with questions and correct answers
        [HttpGet("full/{quizId}")]
        public async Task<ActionResult<QuizDTO>> GetFullAsync(int quizId)
        {
            var quiz = await _context.Quizes
                .FindAsync(quizId);
            if (quiz is null) return BadRequest("Quiz doesnt exist");
            
            var questions = await _context.Questions
                .Where(q => q.Quiz == quiz)
                .Include(q => q.AnswerOptions)
                .ToListAsync();
            //include answers too
            quiz.Questions = questions;

            return _mapper.Map<QuizDTO>(quiz);
        }


        // POST create
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] QuizDTO dto)
        {
            Quiz quiz = new Quiz();
            try
            {
                quiz = _mapper.Map<Quiz>(dto);
            }
            catch
            {
                return BadRequest("Invalid input format");
            }
            
            //check if name and description is not empty
            if (String.IsNullOrEmpty(quiz.Name)) return BadRequest("Quiz name cannot be empty");
            if (String.IsNullOrEmpty(quiz.Description)) return BadRequest("Quiz description cannot be empty");
            //check for at least 1 question
            if (quiz.Questions.Count == 0) return BadRequest("Quiz must have at least 1 question");
            
            foreach (var question in quiz.Questions)
            {
                //check if question string is not empty
                if (String.IsNullOrEmpty(question.QuestionString)) return BadRequest("Question cannot be empty");

                //chek if question type and number of correct answers match
                int correct = 0;
                foreach (var answer in question.AnswerOptions)
                {
                    if (answer.IsCorrect) correct++;
                }

                if (question.QuestionType == QuestionType.OneCorrect && correct != 1)
                    return BadRequest($"Single choice question must have exactly 1 correct answer Question: {question.QuestionString}");
                if(question.QuestionType==QuestionType.MoreCorrect&&correct==0)
                    return BadRequest($"Multiple choice question must have at least 1 correct answer Question: {question.QuestionString}");
            }
            _context.Quizes.Add(quiz);
            var result = await _context.SaveChangesAsync();

            if (result != 0) return Ok();
            else return BadRequest("Cannot save quiz to DB");

        }
    }
}
