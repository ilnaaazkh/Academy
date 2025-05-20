import { useParams } from "react-router";
import MDEditor from "@uiw/react-md-editor";
import { ChangeEvent, useEffect, useState } from "react";
import { useGetLessonQuery } from "../../courses/api";
import {
  Alert,
  Button,
  CircularProgress,
  IconButton,
  Snackbar,
  Typography,
} from "@mui/material";
import {
  useAddLessonAttachmentsMutation,
  useAddPracticeToLessonMutation,
  useRemoveAttachmentMutation,
  useUpdateLessonContentMutation,
} from "./api";
import { skipToken } from "@reduxjs/toolkit/query";
import { CloseSharp } from "@mui/icons-material";
import { CloudUpload } from "@mui/icons-material";
import { PracticeLesson } from "../../courses/components/PracticeLesson";
import Attachment from "../../courses/components/Attachment";

export default function EditableLessonContent() {
  const { courseId, moduleId, lessonId } = useParams();
  const [content, setContent] = useState("");
  const [notification, setNotification] = useState({
    open: false,
    message: "",
    severity: "success" as "success" | "error",
  });

  const { data, isLoading, isError } = useGetLessonQuery(
    lessonId ? { id: lessonId } : skipToken
  );
  const [updateContent, { isLoading: isUpdating }] =
    useUpdateLessonContentMutation();

  const [updatePractice] = useAddPracticeToLessonMutation();
  const [uploadFiles] = useAddLessonAttachmentsMutation();
  const [removeAttachment] = useRemoveAttachmentMutation();

  useEffect(() => {
    if (data?.result) {
      setContent(data.result.content);
    }
  }, [data]);

  async function onSaveContentClick() {
    if (!courseId || !moduleId || !lessonId) return;

    try {
      await updateContent({
        courseId,
        moduleId,
        lessonId,
        content,
      }).unwrap();

      setNotification({
        open: true,
        message: "Изменения успешно сохранены",
        severity: "success",
      });
    } catch {
      setNotification({
        open: true,
        message: "Не удалось сохранить изменения",
        severity: "error",
      });
    }
  }

  async function onAttachmentDelete(fileUrl: string) {
    if (!courseId || !moduleId || !lessonId) return;

    try {
      await removeAttachment({
        courseId,
        moduleId,
        lessonId,
        fileUrl,
      }).unwrap();
    } catch {
      console.log("Failed to delete file");
    }
  }

  async function onSavePracticeClick(templateCode: string) {
    if (!courseId || !moduleId || !lessonId) return;

    try {
      await updatePractice({
        courseId,
        moduleId,
        lessonId,
        templateCode,
      }).unwrap();

      setNotification({
        open: true,
        message: "Изменения успешно сохранены",
        severity: "success",
      });
    } catch {
      setNotification({
        open: true,
        message: "Не удалось сохранить изменения",
        severity: "error",
      });
    }
  }

  async function handleFileUpload(e: ChangeEvent<HTMLInputElement>) {
    const files = e.target.files;
    if (!files || files.length === 0) return;

    if (!courseId || !moduleId || !lessonId) return;

    const filesArray = Array.from(files);
    try {
      await uploadFiles({
        courseId,
        moduleId,
        lessonId,
        files: filesArray,
      }).unwrap();
    } catch {
      setNotification({
        open: true,
        message: "Не удалось загрузить файлы",
        severity: "error",
      });
    }
  }

  const handleCloseNotification = () => {
    setNotification((prev) => ({ ...prev, open: false }));
  };

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
    <div className="mx-24">
      <div className="mb-10 prose mx-auto">
        <h1>{data?.result?.title}</h1>
      </div>

      <div className="mb-5 h-1/2" data-color-mode="light">
        <MDEditor
          value={content}
          height={400}
          textareaProps={{ placeholder: "" }}
          onChange={(val) => setContent(val || "")}
        />
      </div>

      <div className="text-right">
        <Button
          disabled={!content?.trim() || isUpdating}
          variant="contained"
          onClick={onSaveContentClick}
        >
          {isUpdating ? <CircularProgress size={24} /> : "Сохранить"}
        </Button>
      </div>

      <div>
        {data?.result?.lessonType === "PRACTICE" && (
          <PracticeLesson
            onSave={onSavePracticeClick}
            editMode
            data={data.result.practiceLessonData}
          />
        )}
      </div>

      <div className="text-left prose mt-4">
        <h2 className="mb-4">Приложения</h2>
        <div className="flex flex-col gap-3">
          {data?.result?.attachments.map((a) => (
            <Attachment
              key={a.fileUrl}
              fileUrl={a.fileUrl}
              lessonId={lessonId ?? ""}
              onDelete={() => onAttachmentDelete(a.fileUrl)}
              fileName={a.fileName}
            />
          ))}
        </div>
        <div className="mt-4">
          <Button
            variant="outlined"
            component="label"
            startIcon={<CloudUpload />}
          >
            Загрузить файлы
            <input type="file" hidden multiple onChange={handleFileUpload} />
          </Button>
        </div>
      </div>

      <Snackbar
        open={notification.open}
        autoHideDuration={6000}
        onClose={handleCloseNotification}
        anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
      >
        <Alert
          severity={notification.severity}
          action={
            <IconButton
              size="small"
              aria-label="close"
              color="inherit"
              onClick={handleCloseNotification}
            >
              <CloseSharp fontSize="small" />
            </IconButton>
          }
        >
          {notification.message}
        </Alert>
      </Snackbar>
    </div>
  );
}
