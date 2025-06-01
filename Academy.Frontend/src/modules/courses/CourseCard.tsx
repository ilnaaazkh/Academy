import {
  Button,
  Card,
  CardContent,
  Typography,
  Box,
  CardMedia,
} from "@mui/material";
import { CourseDto } from "../../models/response/courseDto";
import { useNavigate } from "react-router-dom";

type Props = {
  course: CourseDto;
};

export function CourseCard({ course }: Props) {
  const navigate = useNavigate();

  return (
    <Card
      sx={{
        height: "400px",
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

      <Box className="px-4">
        <Button
          sx={{ borderRadius: "10px" }}
          variant="contained"
          fullWidth
          size="medium"
          onClick={() => {
            navigate(`/${course.id}`);
          }}
        >
          Начать учиться
        </Button>
      </Box>
    </Card>
  );
}
