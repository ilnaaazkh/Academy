import { useParams } from "react-router";
import MDEditor from "@uiw/react-md-editor";
import { useEffect, useState } from "react";
import { useGetLessonQuery } from "../../courses/api";
import {
  Alert,
  Button,
  CircularProgress,
  IconButton,
  Snackbar,
  Typography,
} from "@mui/material";
import { useUpdateLessonContentMutation } from "./api";
import { skipToken } from "@reduxjs/toolkit/query";
import { CloseSharp } from "@mui/icons-material";

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

  useEffect(() => {
    if (data?.result) {
      setContent(data.result.content);
    }
  }, [data]);

  async function onSaveContentClick() {
    try {
      await updateContent({
        courseId: courseId!,
        moduleId: moduleId!,
        lessonId: lessonId!,
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
      <div className="mb-10">
        <Typography variant="h4">{data?.result?.title}</Typography>
      </div>

      <div className="mb-5 h-1/2" data-color-mode="light">
        <MDEditor value={content} onChange={(val) => setContent(val || "")} />
      </div>

      <div className="text-right">
        <Button
          disabled={!content.trim() || isUpdating}
          variant="contained"
          onClick={onSaveContentClick}
        >
          {isUpdating ? <CircularProgress size={24} /> : "Сохранить"}
        </Button>
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
