import { useDeleteAuthorMutation } from "./api";
import AuthorCard from "./AuthorCard";
import { AuthorResponse } from "./models/AuthorResponse";

interface Props {
  authors?: AuthorResponse[];
}

export default function AuthorsList({ authors }: Props) {
  const [deleteAuthor] = useDeleteAuthorMutation();

  return (
    <div className="space-y-4">
      {authors?.map((author: AuthorResponse) => (
        <AuthorCard onDelete={deleteAuthor} key={author.id} {...author} />
      ))}
    </div>
  );
}
