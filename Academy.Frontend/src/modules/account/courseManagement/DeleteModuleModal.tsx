import { Button, CircularProgress, Typography } from "@mui/material";
import BaseModal from "../../../components/BaseModal";
import { useDeleteModuleMutation } from "./api";
import { useState } from "react";

interface Props {
  isOpen: boolean;
  onClose: () => void;
  moduleId: string;
  courseId: string;
}

export default function DeleteModuleModal({
  isOpen,
  onClose,
  moduleId,
  courseId,
}: Props) {
  const [deleteModule, { isLoading }] = useDeleteModuleMutation();
  const [error, setError] = useState<string>("");

  async function handleClick() {
    try {
      setError("");
      await deleteModule({ courseId, moduleId });
      onClose();
    } catch {
      setError("Не удалось удалить курс");
    }
  }

  return (
    <BaseModal open={isOpen} onClose={onClose} title="Удаление курса">
      <Typography>Вы действительно хотите удалить модуль из курса?</Typography>
      {error && <div className="text-red-500">{error}</div>}
      <Button
        color="error"
        variant="contained"
        onClick={handleClick}
        disabled={isLoading}
        startIcon={isLoading ? <CircularProgress size={20} /> : null}
      >
        {isLoading ? "Удаление..." : "Удалить"}
      </Button>
    </BaseModal>
  );
}
