import {
  Box,
  Typography,
  FormControlLabel,
  Checkbox,
  Alert,
  Button,
  Card,
  CardContent,
} from "@mui/material";
import { useState } from "react";
import { Question } from "../models/lessonDto";

interface Props {
  questions: Question[];
}

export default function TestLesson({ questions }: Props) {
  const [userAnswers, setUserAnswers] = useState<boolean[][]>(
    questions.map((q) => new Array(q.answers.length).fill(false))
  );
  const [showResult, setShowResult] = useState(false);
  const [score, setScore] = useState(0);

  const handleToggle = (qIdx: number, aIdx: number) => {
    if (showResult) return; // блокируем изменение после проверки

    const updated = [...userAnswers];
    updated[qIdx][aIdx] = !updated[qIdx][aIdx];
    setUserAnswers(updated);
  };

  const handleCheck = () => {
    let correct = 0;

    questions.forEach((q, qIdx) => {
      const correctAnswers = q.answers.map((a) => a.isCorrect);
      const userSelection = userAnswers[qIdx];

      const isCorrect =
        correctAnswers.length === userSelection.length &&
        correctAnswers.every((val, i) => val === userSelection[i]);

      if (isCorrect) correct++;
    });

    setScore(correct);
    setShowResult(true);
  };

  const handleReset = () => {
    setUserAnswers(
      questions.map((q) => new Array(q.answers.length).fill(false))
    );
    setShowResult(false);
    setScore(0);
  };

  return (
    <Box className="mt-4">
      {questions.map((q, qIdx) => (
        <Card key={qIdx} className="mb-6 shadow-md">
          <CardContent>
            <Typography variant="h6" className="mb-2 text-left">
              {qIdx + 1}. {q.title}
            </Typography>
            <Box>
              {q.answers.map((a, aIdx) => {
                const checked = userAnswers[qIdx][aIdx];

                const isCorrect = a.isCorrect;
                const wasSelected = checked;

                const isWrong =
                  showResult && wasSelected && isCorrect === false;

                const isRight = showResult && wasSelected && isCorrect === true;

                return (
                  <Box key={aIdx} className="mb-1 ml-4 text-left">
                    <FormControlLabel
                      control={
                        <Checkbox
                          checked={checked}
                          onChange={() => handleToggle(qIdx, aIdx)}
                          disabled={showResult}
                        />
                      }
                      label={
                        <span
                          className={
                            isRight
                              ? "text-green-600 font-semibold"
                              : isWrong
                              ? "text-red-600 font-semibold"
                              : ""
                          }
                        >
                          {a.title}
                        </span>
                      }
                    />
                  </Box>
                );
              })}
            </Box>
          </CardContent>
        </Card>
      ))}

      {showResult && (
        <Alert severity="info" className="mb-4">
          Вы правильно ответили на {score} из {questions.length} вопросов
        </Alert>
      )}

      <Box className="flex justify-end space-x-4">
        {!showResult && (
          <Button onClick={handleCheck} variant="contained" color="primary">
            Проверить
          </Button>
        )}
        {showResult && (
          <Button onClick={handleReset} variant="outlined" color="info">
            Сбросить
          </Button>
        )}
      </Box>
    </Box>
  );
}
