using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Interfaces;
using StudentManagementAPI.Services.enums;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _service;

    public StudentController(StudentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retorna todos os alunos.
    /// </summary>
    /// <returns>Lista de alunos.</returns>
    [HttpGet("all")]
    [SwaggerOperation(Summary = "Retorna todos os alunos cadastrados.")]
    public IActionResult GetAll() => Ok(_service.GetAllStudents());

    /// <summary>
    /// Retorna todos os alunos aprovados.
    /// </summary>
    /// <returns>Lista de alunos aprovados.</returns>
    [HttpGet("approved")]
    [SwaggerOperation(Summary = "Retorna todos os alunos aprovados.")]
    public IActionResult GetApproved() => Ok(_service.GetApprovedStudents());

    /// <summary>
    /// Retorna todos os alunos reprovados.
    /// </summary>
    /// <returns>Lista de alunos reprovados.</returns>
    [HttpGet("disapproved")]
    [SwaggerOperation(Summary = "Retorna todos os alunos reprovados.")]
    public IActionResult GetDisapproved() => Ok(_service.GetDisapprovedStudents());

    /// <summary>
    /// Retorna os dados de um aluno espec�fico baseado na matr�cula.
    /// </summary>
    /// <param name="registration">N�mero da matr�cula do aluno.</param>
    /// <returns>Dados do aluno com a matr�cula fornecida.</returns>
    [HttpGet("{registration}")]
    [SwaggerOperation(Summary = "Retorna os dados de um aluno espec�fico pela matr�cula.")]
    public IActionResult GetByRegistration(string registration)
    {
        var student = _service.GetStudentByRegistration(registration);

        if (student is null)
        {
            return NotFound("N�o existe aluno com essa matr�cula.");
        }

        return Ok(student);
    }

    /// <summary>
    /// Retorna o melhor aluno por mat�ria (maior nota).
    /// </summary>
    /// <returns>Lista com a mat�ria, nome do melhor aluno e sua nota.</returns>
    [HttpGet("best-by-subject")]
    [SwaggerOperation(Summary = "Retorna o melhor aluno por mat�ria.")]
    public IActionResult GetBestStudentsBySubject()
    {
        var bestStudents = _service.GetBestStudentBySubject();

        var result = bestStudents.Select(kvp => new
        {
            Subject = kvp.Key,
            BestStudent = new
            {
                kvp.Value.BestStudent.Name,
                kvp.Value.BestStudent.Registration
            },
            Grade = kvp.Value.Grade
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Endpoint para retornar os alunos ordenados com base na estrat�gia escolhida.
    /// O usu�rio pode escolher a estrat�gia de ordena��o desejada.
    /// </summary>
    /// <param name="strategy">Escolha a estrat�gia de ordena��o. As op��es s�o "Bubble Sort (0)" ou "LINQ Sort (1)".</param>
    /// <returns>Lista de alunos ordenados com base na estrat�gia escolhida.</returns>
    [HttpGet("sorted")]
    [SwaggerOperation(Summary = "Retorna alunos ordenados de acordo com a estrat�gia escolhida.")]
    public IActionResult GetSortedStudents([FromQuery] SortStrategyType strategy)
    {
        try
        {
            var sortedStudents = _service.GetSortedStudentsByAverage(strategy);
            return Ok(sortedStudents);
        }
        catch (ArgumentException)
        {
            return BadRequest("Invalid sorting strategy provided.");
        }
    }
}
