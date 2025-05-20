import {
  Box,
  TextField,
  Checkbox,
  Button,
  IconButton,
  Typography,
  Alert,
} from "@mui/material";
import { Add, Delete, Clear } from "@mui/icons-material";
import { Question } from "../../courses/models/lessonDto";
import { useState } from "react";

interface Props {
  questions: Question[];
  onQuestionsChange: (updatedQuestions: Question[]) => void;
}

export default function EditableTest({ questions, onQuestionsChange }: Props) {
  const [error, setError] = useState<string | null>(null);

  const handleTitleChange = (index: number, title: string) => {
    const updated = questions.map((q, i) =>
      i === index ? { ...q, title } : q
    );
    onQuestionsChange(updated);
  };

  const handleAnswerChange = (
    qIndex: number,
    aIndex: number,
    title: string
  ) => {
    const updated = questions.map((question, i) => {
      if (i !== qIndex) return question;

      const updatedAnswers = question.answers.map((answer, j) =>
        j === aIndex ? { ...answer, title } : answer
      );

      return { ...question, answers: updatedAnswers };
    });

    onQuestionsChange(updated);
  };

  const handleCorrectToggle = (qIndex: number, aIndex: number) => {
    const updated = questions.map((question, i) => {
      if (i !== qIndex) return question;

      const updatedAnswers = question.answers.map((answer, j) =>
        j === aIndex ? { ...answer, isCorrect: !answer.isCorrect } : answer
      );

      return { ...question, answers: updatedAnswers };
    });

    onQuestionsChange(updated);
  };

  const addQuestion = () => {
    onQuestionsChange([
      ...questions,
      {
        title: "",
        answers: [
          { title: "", isCorrect: false },
          { title: "", isCorrect: false },
        ],
      },
    ]);
  };

  const removeQuestion = (index: number) => {
    if (questions.length <= 1) {
      setError("Должен остаться хотя бы один вопрос");
      return;
    }
    setError(null);
    const updated = [...questions];
    updated.splice(index, 1);
    onQuestionsChange(updated);
  };

  const addAnswer = (qIndex: number) => {
    const updated = questions.map((question, i) => {
      if (i !== qIndex) return question;
      return {
        ...question,
        answers: [...question.answers, { title: "", isCorrect: false }],
      };
    });
    onQuestionsChange(updated);
  };

  const removeAnswer = (qIndex: number, aIndex: number) => {
    if (questions[qIndex]?.answers.length <= 2) {
      setError("Должно быть хотя бы 2 варианта ответа");
      return;
    }
    setError(null);

    const updated = questions.map((question, i) => {
      if (i !== qIndex) return question;

      const updatedAnswers = [...question.answers];
      updatedAnswers.splice(aIndex, 1);

      return { ...question, answers: updatedAnswers };
    });

    onQuestionsChange(updated);
  };

  return (
    <Box display="flex" flexDirection="column" gap={4}>
      {error && (
        <Alert severity="error" onClose={() => setError(null)}>
          {error}
        </Alert>
      )}

      {questions.map((question, qIndex) => (
        <Box
          key={`q-${qIndex}`}
          className="border p-4 rounded-md shadow-md"
          sx={{ position: "relative" }}
        >
          <IconButton
            sx={{ position: "absolute", top: 8, right: 8 }}
            onClick={() => removeQuestion(qIndex)}
            color="error"
          >
            <Clear />
          </IconButton>

          <TextField
            fullWidth
            label={`Вопрос ${qIndex + 1}`}
            value={question.title}
            onChange={(e) => handleTitleChange(qIndex, e.target.value)}
            className="mb-4"
            variant="outlined"
            margin="normal"
          />

          <Typography variant="subtitle2" gutterBottom>
            Варианты ответов:
          </Typography>

          {question.answers.map((answer, aIndex) => (
            <Box
              key={`a-${aIndex}`}
              display="flex"
              alignItems="center"
              gap={1}
              mb={1}
              sx={{ position: "relative", pl: 4 }}
            >
              <Checkbox
                checked={answer.isCorrect}
                onChange={() => handleCorrectToggle(qIndex, aIndex)}
                sx={{ position: "absolute", left: 0 }}
              />
              <TextField
                fullWidth
                label={`Ответ ${aIndex + 1}`}
                value={answer.title}
                onChange={(e) =>
                  handleAnswerChange(qIndex, aIndex, e.target.value)
                }
                variant="standard"
              />
              <IconButton
                onClick={() => removeAnswer(qIndex, aIndex)}
                color="error"
                size="small"
              >
                <Delete fontSize="small" />
              </IconButton>
            </Box>
          ))}

          <Button
            startIcon={<Add />}
            onClick={() => addAnswer(qIndex)}
            size="small"
            sx={{ mt: 1 }}
          >
            Добавить ответ
          </Button>
        </Box>
      ))}

      <Button
        variant="outlined"
        startIcon={<Add />}
        onClick={addQuestion}
        sx={{ alignSelf: "flex-start" }}
      >
        Добавить вопрос
      </Button>
    </Box>
  );
}
