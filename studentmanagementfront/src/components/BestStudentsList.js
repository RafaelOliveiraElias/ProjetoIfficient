import React, { useState, useEffect } from "react";
import { Table, Button } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { getBestStudentsBySubject } from "../services/api";

const BestStudentsList = () => {
  const [bestStudents, setBestStudents] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchBestStudents = async () => {
      try {
        const response = await getBestStudentsBySubject();
        setBestStudents(response.data);
      } catch (error) {
        console.error("Erro ao buscar melhores alunos", error);
      } finally {
        setLoading(false);
      }
    };

    fetchBestStudents();
  }, []);

  if (loading) {
    return <p>Carregando...</p>;
  }

  const handleStudentClick = (registration) => {
    navigate(`/student/${registration}`);
  };

  return (
    <div>
      <h2>Melhores Alunos por Matéria</h2>
      <Table striped>
        <thead>
          <tr>
            <th>Matéria</th>
            <th>Aluno</th>
            <th>Nota</th>
            <th>Ação</th>
          </tr>
        </thead>
        <tbody>
          {bestStudents.map((best) => (
            <tr key={best.subject}>
              <td>{best.subject.charAt(0).toUpperCase() + best.subject.slice(1)}</td>
              <td>{best.bestStudent.name}</td>
              <td>{best.grade}</td>
              <td>
                <Button color="primary" onClick={() => handleStudentClick(best.bestStudent.registration)}>
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

export default BestStudentsList;
