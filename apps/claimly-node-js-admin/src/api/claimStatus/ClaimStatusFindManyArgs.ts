import { ClaimStatusWhereInput } from "./ClaimStatusWhereInput";
import { ClaimStatusOrderByInput } from "./ClaimStatusOrderByInput";

export type ClaimStatusFindManyArgs = {
  where?: ClaimStatusWhereInput;
  orderBy?: Array<ClaimStatusOrderByInput>;
  skip?: number;
  take?: number;
};
