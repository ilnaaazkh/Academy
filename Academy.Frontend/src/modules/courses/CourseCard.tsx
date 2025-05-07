import { Button, Card, CardContent, Typography, Box } from "@mui/material";
import { Course } from "./coursesSlice";

type Props = {
  course: Course;
};

export function CourseCard({ course }: Props) {
  return (
    <Card
      sx={{
        height: "300px",
        display: "flex",
        width: "300px",
        borderRadius: "20px",
        flexDirection: "column",
        transition: "transform 0.2s",
        "&:hover": {
          transform: "scale(1.02)",
          boxShadow: 3,
        },
      }}
    >
      <CardContent sx={{ flexGrow: 1 }}>
        <Typography
          variant="h5"
          component="div"
          gutterBottom
          sx={{ fontWeight: 600 }}
        >
          {course.title}
        </Typography>

        <Typography variant="body1" color="text.secondary" paragraph>
          {course.description}
        </Typography>
      </CardContent>

      <Box sx={{ p: 2 }}>
        <Button variant="contained" fullWidth size="medium">
          Начать учиться
        </Button>
      </Box>
    </Card>
  );
}
