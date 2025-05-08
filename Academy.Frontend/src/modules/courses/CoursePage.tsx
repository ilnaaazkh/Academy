import { useParams } from "react-router";

export function CoursePage() {
  const { id } = useParams();
  return <h1>{id}</h1>;
}
