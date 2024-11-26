import * as common from "@nestjs/common";
import * as swagger from "@nestjs/swagger";
import * as nestAccessControl from "nest-access-control";
import { ClaimStatusService } from "./claimStatus.service";
import { ClaimStatusControllerBase } from "./base/claimStatus.controller.base";

@swagger.ApiTags("claimStatuses")
@common.Controller("claimStatuses")
export class ClaimStatusController extends ClaimStatusControllerBase {
  constructor(
    protected readonly service: ClaimStatusService,
    @nestAccessControl.InjectRolesBuilder()
    protected readonly rolesBuilder: nestAccessControl.RolesBuilder
  ) {
    super(service, rolesBuilder);
  }
}
