import { ClaimStatusWhereUniqueInput } from "../claimStatus/ClaimStatusWhereUniqueInput";
import { CustomerWhereUniqueInput } from "../customer/CustomerWhereUniqueInput";

export type ClaimCreateInput = {
  amount?: number | null;
  claimStatus?: ClaimStatusWhereUniqueInput | null;
  customer?: CustomerWhereUniqueInput | null;
  date?: Date | null;
  description?: string | null;
};
