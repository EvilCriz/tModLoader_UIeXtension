using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.UI;

namespace UIeXtension;

public abstract partial class UIExLayout : UIExElement
{
    /// <summary>Флаг состояния, указывающий, нужно ли включать отображение вспомогательных линий для контейнера.</summary>
    public bool ShowLayoutLines = false;

    /// <summary>
    ///     Размер (в пикселях) вспомогательных линий для контейнера.
    ///     Не используется, если <see cref="ShowLayoutLines"/> == false
    /// </summary>
    public float LayoutLinesThickness = 1f;

    /// <summary>
    ///     Цвет вспомогательных линий для контейнера.
    ///     Не используется, если <see cref="ShowLayoutLines"/> == false
    /// </summary>
    public Color LayoutLinesColor = Color.LightGray;

    /// <summary>
    ///     Список областей, содержащих координаты вспомогательных линий.
    ///     <para>Используется, только если <see cref="ShowLayoutLines"/> == true</para>
    /// </summary>
    public List<LayoutDebugRectangle> LayoutDebugRectangles { get; private set; } = new();




    /// <summary>Устанавливает состояние <see cref="ShowLayoutLines"/> для всего древа элементов</summary>
    public void SetLayoutLinesInfoForTree(bool state, float? thickness = null, Color? color = null)
    {
        var root = Utils.UtilsFinder.GetRootParent(this);
        SetLayoutLinesInfoForBranch(root, state, thickness, color);
    }

    /// <summary>Устанавливает состояние <see cref="ShowLayoutLines"/> и его параметры (если переданы) для всей ветки элементов</summary>
    public void SetLayoutLinesInfoForBranch(bool state, float? thickness = null, Color? color = null)
        => SetLayoutLinesInfoForBranch(this, state, thickness, color);

    /// <summary>Устанавливает состояние <see cref="ShowLayoutLines"/> для всего древа элементов</summary>
    private void SetLayoutLinesInfoForBranch(UIElement root, bool state, float? thickness = null, Color? color = null)
    {
        if (root is UIExLayout layout)
        {
            layout.ShowLayoutLines = state;
            if (thickness is not null)
                layout.LayoutLinesThickness = (float)thickness;
            if (color is not null)
                layout.LayoutLinesColor = (Color)color;
        }

        foreach (var child in root.Children)
            SetLayoutLinesInfoForBranch(child, state, thickness, color);
    }




    /// <summary>
    ///     Пересчет расположения вспомогательный линий компоновки элемента.
    /// </summary>
    /// <remarks>
    ///     Ничего не делает, если у <see cref="StyleLayout"/> значение <see cref="ShowLayoutLines"/> == false.
    ///     Вызывается после метода <see cref="PostRefreshLayout"/>
    /// </remarks>
    protected virtual void RefreshLayoutDebugLines()
    {
        LayoutDebugRectangles.Clear();

        if (!ShowLayoutLines)
            return;

        for (int i = 0; i < _elementsContext.Count; i++)
        {
            var element = _elementsContext[i];
            var style = _styleElementsContexts[i].GetCopy();
            style.Width = element.Width;
            style.Height = element.Height;

            float marginTop = style.Margin.Top.GetValue(_innerDimensionsContext.Height);
            float marginLeft = style.Margin.Left.GetValue(_innerDimensionsContext.Width);

            var outer =
                Utils.UtilsLayout.GetForcedCalculatedOuterDimensions(
                    _elementsContext[i],
                    style,
                    _innerDimensionsContext);

            LayoutDebugRectangles.Add(
                new LayoutDebugRectangle(
                    outer.X,
                    outer.Y,
                    outer.Width,
                    outer.Height));
        }
    }




    /// <inheritdoc/>
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        if (ShowLayoutLines)
            foreach (LayoutDebugRectangle layoutDebugRectangle in LayoutDebugRectangles)
                DrawRectangleOutline(spriteBatch, layoutDebugRectangle);
    }

    /// <summary>
    ///     Отрисовка вспомогательных линий компоновки.
    /// </summary>
    /// <param name="spriteBatch"/>
    /// <param name="rectangle">Координаты вспомогательных линий</param>
    protected virtual void DrawRectangleOutline(
        SpriteBatch spriteBatch,
        LayoutDebugRectangle rectangle)
    {
        float thickness = LayoutLinesThickness;

        spriteBatch.Draw(
            TextureAssets.MagicPixel.Value,
            new Rectangle(
                (int)rectangle.X,
                (int)rectangle.Y,
                (int)rectangle.Width,
                (int)thickness),
            LayoutLinesColor);

        spriteBatch.Draw(
            TextureAssets.MagicPixel.Value,
            new Rectangle(
                (int)rectangle.X,
                (int)(rectangle.Y + rectangle.Height - thickness),
                (int)rectangle.Width,
                (int)thickness),
            LayoutLinesColor);

        spriteBatch.Draw(
            TextureAssets.MagicPixel.Value,
            new Rectangle(
                (int)rectangle.X,
                (int)rectangle.Y,
                (int)thickness,
                (int)rectangle.Height),
            LayoutLinesColor);

        spriteBatch.Draw(
            TextureAssets.MagicPixel.Value,
            new Rectangle(
                (int)(rectangle.X + rectangle.Width - thickness),
                (int)rectangle.Y,
                (int)thickness,
                (int)rectangle.Height),
            LayoutLinesColor);
    }
}