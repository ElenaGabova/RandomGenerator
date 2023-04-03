using AutoMapper;
using Domain;
using Entities;
using Inreface;
using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Constants.Constants;

namespace RandomNumbersProject.Controllers
{
    public class HomeController : Controller
    {
        IRandomGeneratorRepository _randomGeneratorRepository;
        INumberRepository _numberRepository;
        IRandomCounterRepository _randomCounterRepository;
        INumberRepetitionRepository _numberRepetitionRepository;
        IMapper _mapper;

        RandomNumberViewModel viewModel = new RandomNumberViewModel();
        public HomeController(IRandomGeneratorRepository randomGeneratorRepository,
                                 IRandomCounterRepository randomCounterRepository,
                                 INumberRepetitionRepository numberRepetitionRepository,
                                 INumberRepository numberRepository,
                                 IMapper mapper, IHttpContextAccessor accessor)
        {
            _randomGeneratorRepository = randomGeneratorRepository;
            _numberRepository          = numberRepository;
            _randomCounterRepository   = randomCounterRepository;
            _numberRepetitionRepository = numberRepetitionRepository;
            _mapper = mapper;
            string methodPath = accessor.HttpContext.Request.Path;

        }


        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ActionResult> Index()
        {
            return View("Index");
        }

        /// <summary>
        /// Генерация последовательности чисел
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /AddGenerateNumbers
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpPost("AddGenerateNumbers")]
        public async Task<IActionResult> AddGenerateNumbers()
        {
            var numbersList = await _randomGeneratorRepository.GenerateNumbers(MinNumber, MaxNumber, NumbersCount);
            var mappedNumbersList = numbersList.Select(n => new Number(n)).ToList();

            Task.Run(() => _numberRepository.AddRangeAsync(mappedNumbersList));

            var numbersCountDictonary = _randomCounterRepository.GetCountNumbers(numbersList);
            var numbersCountToDatabase = numbersCountDictonary.Select(n => new NumberRepetition(n.Key, n.Value)).ToList();
            await _numberRepetitionRepository.AddRangeAsync(numbersCountToDatabase);
          
            List<int> result = await _numberRepetitionRepository.GetNumbersWithTopRepetitions(10);
            viewModel.Result = string.Join(',', result.Select(x => x.ToString()).ToArray());

            return View("RandomNumbersView", viewModel);
        }

        /// Подсчет количества чисел
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /AddCountNumbers
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpPost("AddCountNumbers")]
        public async Task<IActionResult> AddCountNumbers()
        {
            var numbersFromDatabase = await _numberRepository.GetAsync();
            var mappedNumbersFromDatabaseArray = numbersFromDatabase.Select(n => n.NumberValue).ToList();

            var numbersCountDictonary = _randomCounterRepository.GetCountNumbers(mappedNumbersFromDatabaseArray);
            var numbersCountToDatabase = numbersCountDictonary.Select(n => new NumberRepetition(n.Key, n.Value)).ToList();
            await _numberRepetitionRepository.AddRangeAsync(numbersCountToDatabase);
            return Ok();
        }

        /// Подсчет количества повторений числа в последовательности
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /GetNumberByNumberRepetitions
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <param name="number">Число</param>
        /// <returns>Количество повторений чисел</returns>
        /// <response code="200">Успешное выполнение</response>
    //    [HttpGet("GetNumberRepetitionByNumber")]
        public async Task<IActionResult> GetNumberRepetitionByNumber(int number)
        {
            int result = await _numberRepetitionRepository.GetNumberRepetitionByNumber(number);
            viewModel.Result = result.ToString();
            return View("RandomNumbersView", viewModel);
        }

        /// Подсчет количества чисел, с заданным количеством повторений последовательности
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /GetNumberByNumberRepetitions
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <param name="repetition">Количество повторений чисел</param>
        /// <returns>Числа, с таким количеством повторений</returns>
        /// <response code="200">Успешное выполнение</response>
    //    [HttpGet("GetNumberByNumberRepetitions")]
        public async Task<IActionResult> GetNumberByNumberRepetitions(int repetitionNumber)
        {
            List<int> result = await _numberRepetitionRepository.GetNumberByNumberRepetitions(repetitionNumber);
            viewModel.Result = string.Join(',', result.Select(x => x.ToString()).ToArray());
            return View("RandomNumbersView", viewModel);
        }

        /// Вывод топ чисел с самым большим количеством повторений
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /GetNumbersWithTopRepetitions
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <param name="topCount">Количество чисел, которое нужно вывести</param>
        /// <returns>Числа  с самым большим количеством повторений</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("GetNumbersWithTopRepetitions")]
        public async Task<IActionResult> GetNumbersWithTopRepetitions(int topCount = 10)
        {
            List<int> result = await _numberRepetitionRepository.GetNumbersWithTopRepetitions(topCount);
            viewModel.Result = string.Join( ',', result.Select(x => x.ToString()).ToArray());
            return View("RandomNumbersView", viewModel);
        }
    }
}
