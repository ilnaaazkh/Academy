import { zodResolver } from "@hookform/resolvers/zod";
import BaseModal from "../../../components/BaseModal";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { Button, CircularProgress, TextField } from "@mui/material";
import { useAddModuleMutation } from "./api";

interface Props {
  isOpen: boolean;
  onClose: () => void;
  courseId: string;
}

const schema = z.object({
  title: z
    .string()
    .nonempty({ message: "Это поле обязательно для заполнения" }),
});

type FormFields = z.infer<typeof schema>;

export default function AddModuleModal({ isOpen, onClose, courseId }: Props) {
  const {
    register,
    reset,
    handleSubmit,
    setError,
    formState: { errors, isSubmitting },
  } = useForm<FormFields>({ resolver: zodResolver(schema) });

  const [addModule] = useAddModuleMutation();

  async function onSubmit({ title }: FormFields) {
    try {
      await addModule({ courseId, title }).unwrap();
      onCloseWrapper();
    } catch {
      setError("root", {
        type: "manual",
        message: "Ошибка при создании модуля",
      });
    }
  }

  function onCloseWrapper() {
    reset();
    onClose();
  }

  return (
    <BaseModal open={isOpen} onClose={onCloseWrapper} title="Добавить модуль">
      <form className="flex flex-col gap-3" onSubmit={handleSubmit(onSubmit)}>
        <TextField
          {...register("title")}
          label="Название курса"
          error={!!errors.title}
          helperText={errors.title?.message}
          variant="outlined"
        ></TextField>

        {errors.root?.message && (
          <div className="text-red-500">{errors.root?.message}</div>
        )}

        <Button
          variant="contained"
          type="submit"
          disabled={isSubmitting}
          startIcon={isSubmitting ? <CircularProgress /> : null}
        >
          {isSubmitting ? "Загрузка" : "Добавить модуль"}
        </Button>
      </form>
    </BaseModal>
  );
}
