import React, { useState, useEffect } from "react";
import { Card, CardBody, CardTitle, CardText, Table, Spinner } from "reactstrap";
import { useParams } from "react-router-dom";
import { getStudentByRegistration } from "../services/api";

const StudentDetails = () => {
  const { registration } = useParams();
  const [student, setStudent] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchStudentDetails = async () => {
      try {
        const response = await getStudentByRegistration(registration);
        setStudent(response.data);
      } catch (error) {
        console.error("Erro ao buscar os detalhes do aluno", error);
      } finally {
        setLoading(false);
      }
    };

    fetchStudentDetails();
  }, [registration]);

  if (loading) {
    return <Spinner color="primary" />;
  }

  if (!student || !student.grades) {
    return <p>Aluno não encontrado ou sem notas.</p>;
  }

  const isGradeBelow60 = (grade) => grade < 60;
  const isApproved = Object.values(student.grades).every(grade => grade >= 60);

  return (
    <div>
      <Card>
        <CardBody>
          <CardTitle tag="h5">{student.name}</CardTitle>
          <CardText><strong>Matrícula:</strong> {student.registration}</CardText>
          <h5>Notas por Matéria</h5>
          <Table striped>
            <thead>
              <tr>
                <th>Matéria</th>
                <th>Nota</th>
              </tr>
            </thead>
            <tbody>
              {Object.entries(student.grades).map(([subject, grade], index) => (
                <tr key={index}>
                  <td>{subject.charAt(0).toUpperCase() + subject.slice(1)}</td> {/* Primeira letra maiúscula */}
                  <td style={{
                    backgroundColor: isGradeBelow60(grade) ? 'red' : 'transparent',
                    color: isGradeBelow60(grade) ? 'white' : 'black'
                  }}>
                    {grade}
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
          <CardText>
            <strong>Nota Média:</strong> {student.average.toFixed(2)}
          </CardText>
          <CardText style={{ color: isApproved ? 'green' : 'red' }}>
            <strong>Status: </strong> {isApproved ? "Aprovado" : "Reprovado"}
          </CardText>
        </CardBody>
      </Card>
    </div>
  );
};

export default StudentDetails;
