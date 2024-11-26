import { Injectable } from "@nestjs/common";
import { PrismaService } from "../prisma/prisma.service";
import { ClaimStatusServiceBase } from "./base/claimStatus.service.base";

@Injectable()
export class ClaimStatusService extends ClaimStatusServiceBase {
  constructor(protected readonly prisma: PrismaService) {
    super(prisma);
  }
}
