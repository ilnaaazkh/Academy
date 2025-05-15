import { useForm } from "react-hook-form";
import BaseModal from "../../../components/BaseModal";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { Button, CircularProgress, TextField } from "@mui/material";
import { useCreateCourseMutation } from "./api";

interface Props {
  isOpen: boolean;
  onClose: () => void;
}

const schema = z.object({
  title: z
    .string()
    .nonempty({ message: "Это поле обязательно для заполнения" }),
  description: z
    .string()
    .nonempty({ message: "Это поле обязательно для заполнения" })
    .max(100),
});

type FormFields = z.infer<typeof schema>;

export default function CreateCourseModal({ isOpen, onClose }: Props) {
  const {
    register,
    handleSubmit,
    setError,
    reset,
    formState: { isSubmitting, errors },
  } = useForm<FormFields>({ resolver: zodResolver(schema) });

  const [createCourse] = useCreateCourseMutation();
  const onCloseWrapper = () => {
    reset();
    onClose();
  };

  const onSubmit = async ({ title, description }: FormFields) => {
    try {
      await createCourse({ title, description }).unwrap();
      reset();
      onClose();
    } catch {
      setError("root", { message: "Не удалось создать курс" });
    }
  };

  return (
    <BaseModal open={isOpen} onClose={onCloseWrapper} title="Создание курса">
      <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col gap-3">
        <TextField
          {...register("title")}
          label="Название курса"
          error={!!errors.title}
          helperText={errors.title?.message}
          variant="outlined"
        ></TextField>
        <TextField
          {...register("description")}
          label="Описание"
          error={!!errors.description}
          helperText={errors.description?.message}
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
          {isSubmitting ? "Загрузка" : "Создать"}
        </Button>
      </form>
    </BaseModal>
  );
}
