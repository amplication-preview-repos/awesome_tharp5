import { ClaimCreateNestedManyWithoutClaimStatusesInput } from "./ClaimCreateNestedManyWithoutClaimStatusesInput";

export type ClaimStatusCreateInput = {
  claims?: ClaimCreateNestedManyWithoutClaimStatusesInput;
  status?: string | null;
};
