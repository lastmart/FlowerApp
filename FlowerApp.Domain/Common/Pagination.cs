namespace FlowerApp.Domain.Common;

public record Pagination(int Skip = 0, int Take = 50);