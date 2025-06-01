import {
  Button,
  Card,
  CardContent,
  Typography,
  CardMedia,
  Box,
} from "@mui/material";
import { CourseDto } from "../../../../models/response/courseDto";
import { useNavigate } from "react-router-dom";
import { usePublishCourseMutation, useRejectCourseMutation } from "../api";

type Props = {
  course: CourseDto;
};

export function ModerateCourseCard({ course }: Props) {
  const navigate = useNavigate();
  const [publishCourse, { isLoading: isPublishing }] =
    usePublishCourseMutation();
  const [reject, { isLoading: isRejecting }] = useRejectCourseMutation();

  const handlePublish = async () => {
    try {
      await publishCourse({ id: course.id }).unwrap();
    } catch {
      console.log("Не удалось опубликовать курс");
    }
  };

  const handleReject = async () => {
    try {
      await reject({ id: course.id }).unwrap();
    } catch {
      console.log("Не удалось отклонить запрос на публикацию");
    }
  };

  return (
    <Card
      sx={{
        height: "450px",
        display: "flex",
        width: "400px",
        borderRadius: "20px",
        flexDirection: "column",
        transition: "transform 0.2s",
        "&:hover": {
          transform: "scale(1.02)",
          boxShadow: 3,
        },
      }}
    >
      <CardMedia
        component="img"
        height="160"
        image={course.preview}
        alt={course.title}
        sx={{
          width: "100%",
          objectFit: "cover",
          aspectRatio: "16/9",
          borderTopLeftRadius: "20px",
          borderTopRightRadius: "20px",
        }}
      />
      <CardContent sx={{ flexGrow: 1 }}>
        <Typography
          variant="h5"
          component="div"
          gutterBottom
          sx={{ fontWeight: 300 }}
        >
          {course.title}
        </Typography>

        <Typography variant="body1" color="text.secondary">
          {course.description}
        </Typography>
      </CardContent>

      <Box className="px-4 flex flex-col gap-1">
        <Button
          sx={{ borderRadius: "10px" }}
          variant="outlined"
          fullWidth
          size="medium"
          onClick={() => {
            navigate(`/moderate/${course.id}`);
          }}
        >
          Просмотреть
        </Button>
        <div className="flex gap-1 justify-between">
          <Button
            sx={{ borderRadius: "10px" }}
            variant="contained"
            color="success"
            size="medium"
            disabled={isPublishing || isRejecting}
            onClick={handlePublish}
          >
            Опубликовать
          </Button>
          <Button
            sx={{ borderRadius: "10px" }}
            variant="contained"
            color="error"
            size="medium"
            disabled={isPublishing || isRejecting}
            onClick={handleReject}
          >
            Отклонить
          </Button>
        </div>
      </Box>
    </Card>
  );
}
