import { zodResolver } from "@hookform/resolvers/zod";
import BaseModal from "../../../components/BaseModal";
import { z } from "zod";
import { useForm } from "react-hook-form";
import {
  Button,
  CircularProgress,
  MenuItem,
  Select,
  TextField,
  InputLabel,
  FormControl,
  FormHelperText,
} from "@mui/material";
import { useAddLessonMutation } from "./api";

interface Props {
  isOpen: boolean;
  onClose: () => void;
  courseId: string;
  moduleId: string;
}

const schema = z.object({
  title: z.string().nonempty({ message: "Это поле обязательно" }),
  lessonType: z.enum(["LECTURE", "PRACTICE", "TEST"], {
    errorMap: () => ({ message: "Выберите тип урока" }),
  }),
});

type FormFields = z.infer<typeof schema>;

export default function AddLessonModal({
  isOpen,
  onClose,
  courseId,
  moduleId,
}: Props) {
  const {
    register,
    reset,
    handleSubmit,
    setError,
    formState: { errors, isSubmitting },
  } = useForm<FormFields>({
    resolver: zodResolver(schema),
  });

  const [addLesson] = useAddLessonMutation();

  async function onSubmit(data: FormFields) {
    try {
      await addLesson({ ...data, courseId, moduleId }).unwrap();
      onCloseWrapper();
    } catch {
      setError("root", {
        type: "manual",
        message: "Ошибка при создании урока",
      });
    }
  }

  function onCloseWrapper() {
    reset();
    onClose();
  }

  return (
    <BaseModal open={isOpen} onClose={onCloseWrapper} title="Добавить урок">
      <form className="flex flex-col gap-4" onSubmit={handleSubmit(onSubmit)}>
        <TextField
          {...register("title")}
          label="Название урока"
          error={!!errors.title}
          helperText={errors.title?.message}
          variant="outlined"
        />

        <FormControl error={!!errors.lessonType}>
          <InputLabel id="lessonType-label">Тип урока</InputLabel>
          <Select
            labelId="lessonType-label"
            defaultValue=""
            {...register("lessonType")}
            label="Тип урока"
          >
            <MenuItem value="LECTURE">Лекция</MenuItem>
            <MenuItem value="PRACTICE">Практика</MenuItem>
            <MenuItem value="TEST">Тест</MenuItem>
          </Select>
          <FormHelperText>{errors.lessonType?.message}</FormHelperText>
        </FormControl>

        {errors.root?.message && (
          <div className="text-red-500">{errors.root.message}</div>
        )}

        <Button
          type="submit"
          variant="contained"
          disabled={isSubmitting}
          startIcon={isSubmitting ? <CircularProgress size={20} /> : null}
        >
          {isSubmitting ? "Создание..." : "Добавить урок"}
        </Button>
      </form>
    </BaseModal>
  );
}
