import { Button, CircularProgress, Typography } from "@mui/material";
import BaseModal from "../../../components/BaseModal";
import { useDeleteLessonMutation } from "./api";
import { useState } from "react";

interface Props {
  isOpen: boolean;
  onClose: () => void;
  lessonId: string;
  moduleId: string;
  courseId: string;
  lessonTitle: string;
}

export default function DeleteLessonModal({
  isOpen,
  onClose,
  lessonId,
  moduleId,
  courseId,
  lessonTitle,
}: Props) {
  const [deleteLesson, { isLoading }] = useDeleteLessonMutation();
  const [error, setError] = useState<string>("");

  async function handleClick() {
    try {
      setError("");
      await deleteLesson({ courseId, moduleId, lessonId }).unwrap();
      onClose();
    } catch {
      setError("Не удалось удалить урок");
    }
  }

  return (
    <BaseModal open={isOpen} onClose={onClose} title="Удаление урока">
      <Typography>
        Вы действительно хотите удалить урок <strong>{lessonTitle}</strong>?
      </Typography>
      {error && <div className="text-red-500 mt-2">{error}</div>}
      <Button
        color="error"
        variant="contained"
        onClick={handleClick}
        disabled={isLoading}
        startIcon={isLoading ? <CircularProgress size={20} /> : null}
        className="mt-4"
      >
        {isLoading ? "Удаление..." : "Удалить"}
      </Button>
    </BaseModal>
  );
}
