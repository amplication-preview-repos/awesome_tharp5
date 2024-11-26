import { Module, forwardRef } from "@nestjs/common";
import { AuthModule } from "../auth/auth.module";
import { ClaimStatusModuleBase } from "./base/claimStatus.module.base";
import { ClaimStatusService } from "./claimStatus.service";
import { ClaimStatusController } from "./claimStatus.controller";
import { ClaimStatusResolver } from "./claimStatus.resolver";

@Module({
  imports: [ClaimStatusModuleBase, forwardRef(() => AuthModule)],
  controllers: [ClaimStatusController],
  providers: [ClaimStatusService, ClaimStatusResolver],
  exports: [ClaimStatusService],
})
export class ClaimStatusModule {}
