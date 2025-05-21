import { useNavigate, useParams } from "react-router";
import {
  useDeleteCourseMutation,
  useGetCourseInfoQuery,
  useUpdateCourseMutation,
  useUploadCoursePreviewMutation,
} from "./api";
import { skipToken } from "@reduxjs/toolkit/query";
import { Alert, Button, CircularProgress, TextField } from "@mui/material";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { ChangeEvent, useEffect, useState } from "react";
import DeleteCourseModal from "./DeleteCourseModal";

const schema = z.object({
  title: z.string().nonempty("Это поле обязательно для заполнения"),
  description: z.string().nonempty("Это поле обязательно для заполнения"),
});

type FormFields = z.infer<typeof schema>;

export default function EditMainInfo() {
  const {
    register,
    reset,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<FormFields>({ resolver: zodResolver(schema) });
  const { courseId } = useParams();
  const { data, isLoading, isError } = useGetCourseInfoQuery(
    courseId ? { id: courseId } : skipToken
  );
  const [deleteModalOpen, setDeleteModalOpen] = useState(false);
  const navigate = useNavigate();
  const [uploadPreview] = useUploadCoursePreviewMutation();
  const [updateCourse] = useUpdateCourseMutation();
  const [deleteCourse] = useDeleteCourseMutation();

  async function handleUploadPreview(e: ChangeEvent<HTMLInputElement>) {
    if (!courseId) return;

    const file = e.target?.files?.[0];
    if (!file) return;

    try {
      await uploadPreview({ courseId, file }).unwrap();
    } catch {
      console.log("Не удалось загрузить обложку курса");
    }
  }

  useEffect(() => {
    if (data?.result) {
      reset({ title: data.result.title, description: data.result.description });
    }
  }, [data, reset]);

  async function onSubmit(data: FormFields) {
    if (!courseId) return;

    try {
      await updateCourse({
        id: courseId,
        title: data.title,
        description: data.description,
      }).unwrap();
    } catch {
      console.log("Не удалось сохранить изменения");
    }
  }

  async function onDeleteConfirm() {
    if (!courseId) return;

    try {
      await deleteCourse({ id: courseId }).unwrap();
      setDeleteModalOpen(false);
      navigate("/profile/own-courses");
    } catch {
      console.log("Не удалось удалить курс");
    }
  }

  if (isLoading) {
    return <CircularProgress />;
  }

  if (isError) {
    return (
      <div className="h-3/4 flex justify-center items-center m-10">
        <Alert severity="error" variant="outlined">
          Ошибка при получении данных. Попробуйте позже
        </Alert>
      </div>
    );
  }

  return (
    <>
      <div className="flex m-14 gap-6 items-stretch">
        <div className="w-1/3 m-2 flex flex-col gap-4">
          <div className="min-h-[300px] bg-slate-200 flex justify-center items-center rounded overflow-hidden">
            {data?.result?.preview ? (
              <img src={data.result.preview} alt={data.result.title}></img>
            ) : (
              <span className="text-center p-4 text-gray-600">
                Загрузите обложку курса
              </span>
            )}
          </div>
          <Button
            variant="outlined"
            component="label"
            className="w-fit self-center"
          >
            Загрузить
            <input
              type="file"
              hidden
              onChange={handleUploadPreview}
              accept=".png,.jpg,.jpeg"
            />
          </Button>
        </div>
        <div className="w-2/3 m-2">
          <form
            onSubmit={handleSubmit(onSubmit)}
            className="flex flex-col gap-4"
          >
            <TextField
              defaultValue=""
              label="Название курса"
              {...register("title")}
              error={!!errors.title}
              helperText={errors.title?.message}
            />
            <TextField
              defaultValue=""
              label="Описание курса"
              multiline
              minRows={4}
              {...register("description")}
              error={!!errors.description}
              helperText={errors.description?.message}
            />
            <Button
              type="submit"
              variant="contained"
              disabled={isSubmitting}
              className="w-fit self-center"
            >
              Сохранить
            </Button>
          </form>
        </div>
      </div>
      <div>
        <Button
          className="w-fit self-end"
          color="error"
          variant="contained"
          onClick={() => setDeleteModalOpen(true)}
        >
          Удалить курс
        </Button>
      </div>
      <DeleteCourseModal
        onClose={() => setDeleteModalOpen(false)}
        isOpen={deleteModalOpen}
        onConfirm={onDeleteConfirm}
      />
    </>
  );
}
