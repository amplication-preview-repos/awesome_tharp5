import { ClaimWhereInput } from "./ClaimWhereInput";
import { ClaimOrderByInput } from "./ClaimOrderByInput";

export type ClaimFindManyArgs = {
  where?: ClaimWhereInput;
  orderBy?: Array<ClaimOrderByInput>;
  skip?: number;
  take?: number;
};
