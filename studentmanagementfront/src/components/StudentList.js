import React, { useState, useEffect } from "react";
import { Table, Input, Button } from "reactstrap";  // Adicionando o Button
import { useNavigate } from "react-router-dom";  // Usando para navegação
import { getSortedStudents } from "../services/api";

const StudentList = () => {
  const [students, setStudents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [strategy, setStrategy] = useState(0); // Armazena a estratégia selecionada
  const navigate = useNavigate(); // Hook de navegação

  useEffect(() => {
    const fetchStudents = async () => {
      try {
        const response = await getSortedStudents(strategy);
        setStudents(response.data);
      } catch (error) {
        console.error("Erro ao buscar alunos", error);
      } finally {
        setLoading(false);
      }
    };

    fetchStudents();
  }, [strategy]);

  if (loading) {
    return <p>Carregando...</p>;
  }

  const handleStudentClick = (registration) => {
    // Redireciona para o aluno selecionado com base na matrícula
    navigate(`/student/${registration}`);
  };

  return (
    <div>
      <h2>Lista de Alunos</h2>
      <Input 
        type="select" 
        value={strategy} 
        onChange={(e) => setStrategy(Number(e.target.value))} 
      >
        <option value={0}>Bubble Sort</option>
        <option value={1}>LINQ Sort</option>
      </Input>

      <Table striped>
        <thead>
          <tr>
            <th>Matrícula</th>
            <th>Nome</th>
            <th>Nota Média</th>
            <th>Ação</th> {/* Coluna para o botão */}
          </tr>
        </thead>
        <tbody>
          {students.map((student) => (
            <tr key={student.registration}>
              <td>{student.registration}</td>
              <td>{student.name}</td>
              <td>{student.average}</td>
              <td>
                {/* Botão para navegar para os detalhes do aluno */}
                <Button color="primary" onClick={() => handleStudentClick(student.registration)}>
                  Ver Detalhes
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
};

export default StudentList;
